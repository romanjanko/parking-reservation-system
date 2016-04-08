using System;
using System.Linq;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Convertors;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class GarageOnMondayOrFridayReservationRule : AbstractReservationRule
    {
        public GarageOnMondayOrFridayReservationRule(IUnitOfWork unitOfWork,
                                                     IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                                     IWeekOfYearToDateConvertor weekOfYearToDateConvertor,
                                                     ICurrentTime currentTime)
            : base(unitOfWork, dateToWeekOfYearConvertor, weekOfYearToDateConvertor, currentTime)
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsRightTimeToRemoveAllRestrictions(reservation) ||
                IsReservationMadeByAdminUser(reservation))
                return new SuccessfullFreeGarageReservation();

            if (IsReservationMadeForOutsideParkingSpot(reservation))
                return new SuccessfullCommonReservation();

            var userReservationsInGarage = GetUserReservationsInGarageForWeek(
                reservation.ApplicationUser, reservation.ReservationDate);

            if (IsReservationForMonday(reservation))
            {
                var garageReservationOnFriday = userReservationsInGarage
                    .FirstOrDefault(IsReservationForFriday);

                if (garageReservationOnFriday != null)
                {
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

        private IEnumerable<Reservation> GetUserReservationsInGarageForWeek(
            ApplicationUser user, DateTime dateOfDayInWeek)
        {
            DateTime startDateOfWeek, endDateOfWeek;

            GetStartAndEndBusinessDayOfWeek(dateOfDayInWeek, out startDateOfWeek, out endDateOfWeek);

            return UnitOfWork.Reservations.GetNonFreeGarageReservationsByUser(user, startDateOfWeek, endDateOfWeek);
        }
    }
}
