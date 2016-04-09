using System;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public abstract class AbstractReservationRule : IReservationRule
    {
        protected readonly TimeSpan NoonThreshold;
        protected readonly int GarageLimitPerWeek;

        protected AbstractReservationRule()
        {
            NoonThreshold = new TimeSpan(12, 0, 0);
            GarageLimitPerWeek = 2;
        }
        
        public abstract ReservationValidationResult Validate(Reservation reservation);

        public bool IsRightTimeToRemoveAllRestrictions(Reservation reservation)
        {
            var shiftedReservationDate = new DateTime(reservation.ReservationDate.Year,
                                                      reservation.ReservationDate.Month,
                                                      reservation.ReservationDate.Day,
                                                      NoonThreshold.Hours,
                                                      NoonThreshold.Minutes,
                                                      NoonThreshold.Seconds);

            if (IsReservationMadeForMonday(reservation))
                shiftedReservationDate = shiftedReservationDate.AddDays(-3);
            else
                shiftedReservationDate = shiftedReservationDate.AddDays(-1);

            return reservation.CreatedDate >= shiftedReservationDate;
        }
        
        protected bool IsReservationMadeForOutsideParkingSpot(Reservation reservation)
        {
            return reservation.ParkingSpot.Type == ParkingSpotType.Outside;
        }

        protected bool IsReservationMadeByAdminUser(Reservation reservation)
        {
            return reservation.ApplicationUser.IsUserAdmin();
        }

        protected bool IsReservationMadeForMonday(Reservation reservation)
        {
            return reservation.ReservationDate.DayOfWeek == DayOfWeek.Monday;
        }
    }
}
