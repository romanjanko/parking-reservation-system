using ParkingSystem.Core.RepositoryAbstraction;
using ParkingSystem.Core.Utils;
using System;
using System.Linq;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageOnMondayOrFridayReservationRule : AbstractReservationRule
    {
        public GarageOnMondayOrFridayReservationRule(IUnitOfWork unitOfWork,
                                                     IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                                     IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
            : base(unitOfWork, dateToWeekOfYearConvertor, weekOfYearToDateConvertor)
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsRightTimeToRemoveAllRestrictions(reservation) ||
                IsReservationBeingMadeForOutsideParkingSpot(reservation) ||
                IsReservationBeingMadeByAdminUser(reservation))
                return new SuccessfullReservationValidationResult();
                        
            var userReservationsInGarage = GetUserReservationsInGarageForWeek(
                reservation.ApplicationUser, reservation.ReservationDate);

            if (IsReservationForMonday(reservation))
            {
                var garageReservationOnFriday = userReservationsInGarage
                    .FirstOrDefault(r => IsReservationForFriday(r));

                if (garageReservationOnFriday != null)
                {
                    return new FailedReservationValidationResult(@"You can sign up for either Monday or Friday in garage.
                        A reservation for Friday was already made.");
                }
            }
            else if (IsReservationForFriday(reservation))
            {
                var garageReservationOnMonday = userReservationsInGarage
                    .FirstOrDefault(r => IsReservationForMonday(r));

                if (garageReservationOnMonday != null)
                {
                    return new FailedReservationValidationResult(@"You can sign up for either Monday or Friday in garage. 
                        A reservation for Monday was already made.");
                }
            }

            return new SuccessfullReservationValidationResult();
        }

        private bool IsReservationForMonday(Reservation reservation)
        {
            return reservation.ReservationDate.DayOfWeek == DayOfWeek.Monday;
        }

        private bool IsReservationForFriday(Reservation reservation)
        {
            return reservation.ReservationDate.DayOfWeek == DayOfWeek.Friday;
        }

        private IList<Reservation> GetUserReservationsInGarageForWeek(
            ApplicationUser user, DateTime dateOfDayInWeek)
        {
            DateTime startDateOfWeek, endDateOfWeek;

            GetStartAndEndBusinessDayOfWeek(dateOfDayInWeek, out startDateOfWeek, out endDateOfWeek);

            return _unitOfWork.Reservations.GetGarageReservationsByUser(user, startDateOfWeek, endDateOfWeek);
        }
    }
}
