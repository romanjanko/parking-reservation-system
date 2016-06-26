using System;
using System.Linq;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.ReservationRules.AntiCheatingPolicies;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageOnMondayOrFridayReservationRule : AbstractReservationRule
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;
        private readonly ICheatingCheck _cheatingCheck;

        public GarageOnMondayOrFridayReservationRule(IUnitOfWork unitOfWork,
                                                     IDatesOfBusinessDaysCounter datesOfBusinessDaysCounter,
                                                     ICheatingCheck cheatingCheck)
            : base()
        {
            _unitOfWork = unitOfWork;
            _datesOfBusinessDaysCounter = datesOfBusinessDaysCounter;
            _cheatingCheck = cheatingCheck;
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationMadeForOutsideParkingSpot(reservation))
                return new SuccessfullCommonReservation();

            if (IsReservationMadeByAdminUser(reservation))
                return new SuccessfullFreeGarageReservation();

            var cheatingCheckResult = _cheatingCheck.CheckForCheating(reservation);
            var rightTimeRemoveRestrictions = IsRightTimeToRemoveAllRestrictions(reservation);

            if (rightTimeRemoveRestrictions &&
                cheatingCheckResult.CheatingDetected == false)
                return new SuccessfullFreeGarageReservation();

            var userReservationsInGarage = GetUserReservationsInGarageForWeek(
                reservation.ApplicationUser, reservation.ReservationDate);

            if (IsReservationForMonday(reservation))
            {
                var garageReservationOnFriday = userReservationsInGarage
                    .FirstOrDefault(IsReservationForFriday);

                if (garageReservationOnFriday != null)
                {
                    if (rightTimeRemoveRestrictions && cheatingCheckResult.CheatingDetected)
                        return new FailedReservation(@"Possible cheating detected based on your previous reservations for this day. 
                            You cannot reserve the parking spot freely.");

                    return new FailedReservation(@"You can sign up for either Monday or Friday in garage.
                        A reservation for Friday was already made.");
                }
            }
            else if (IsReservationForFriday(reservation))
            {
                var garageReservationOnMonday = userReservationsInGarage
                    .FirstOrDefault(IsReservationForMonday);

                if (garageReservationOnMonday != null)
                {
                    if (rightTimeRemoveRestrictions && cheatingCheckResult.CheatingDetected)
                        return new FailedReservation(@"Possible cheating detected based on your previous reservations for this day. 
                            You cannot reserve the parking spot freely.");

                    return new FailedReservation(@"You can sign up for either Monday or Friday in garage. 
                        A reservation for Monday was already made.");
                }
            }

            return new SuccessfullNonFreeGarageReservation();
        }

        private bool IsReservationForMonday(Reservation reservation)
        {
            return reservation.ReservationDate.DayOfWeek == DayOfWeek.Monday;
        }

        private bool IsReservationForFriday(Reservation reservation)
        {
            return reservation.ReservationDate.DayOfWeek == DayOfWeek.Friday;
        }

        private IEnumerable<Reservation> GetUserReservationsInGarageForWeek(ApplicationUser user, DateTime dateOfDayInWeek)
        {
            var datesOfBusinessDaysInWeek = _datesOfBusinessDaysCounter.GetDatesOfBusinessDaysInWeek(dateOfDayInWeek);
            
            return _unitOfWork.Reservations.GetNonFreeGarageReservationsByUser(user,
                datesOfBusinessDaysInWeek.Monday, datesOfBusinessDaysInWeek.Friday);
        }
    }
}
