using System;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.RepositoryAbstraction;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class NoPastDatesReservationRule : AbstractReservationRule
    {
        public NoPastDatesReservationRule()
            : base()
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationBeingMadeForPastDate(reservation) &&
               !IsReservationBeingMadeByAdminUser(reservation))
                return new FailedReservationValidationResult("Reservations cannot be made for past dates.");
            else
                return new SuccessfullReservationValidationResult();
        }

        private bool IsReservationBeingMadeForPastDate(Reservation reservation)
        {
            return reservation.ReservationDate < DateTime.Today;
        }
    }
}
