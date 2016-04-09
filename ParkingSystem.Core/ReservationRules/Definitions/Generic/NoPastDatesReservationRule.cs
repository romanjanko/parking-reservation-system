using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.Time;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class NoPastDatesReservationRule : AbstractReservationRule
    {
        private readonly ICurrentTime _currentTime;

        public NoPastDatesReservationRule(ICurrentTime currentTime)
            : base()
        {
            _currentTime = currentTime;
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationBeingMadeForPastDate(reservation) &&
               !IsReservationMadeByAdminUser(reservation))
                return new FailedReservation("Reservations cannot be made for past dates.");
            else
                return new SuccessfullCommonReservation();
        }

        private bool IsReservationBeingMadeForPastDate(Reservation reservation)
        {
            return reservation.ReservationDate.Date < _currentTime.Now().Date;
        }
    }
}
