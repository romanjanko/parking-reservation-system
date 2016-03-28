using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class OnlyFreeParkingSpotReservationRule : AbstractReservationRule
    {
        public OnlyFreeParkingSpotReservationRule(IUnitOfWork unitOfWork)
            : base(unitOfWork, null, null, null)
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationParkingSpotStillFree(reservation))
                return new SuccessfullReservationValidationResult();
            else
                return new FailedReservationValidationResult("The parking spot is already reserved for this day.");
        }

        private bool IsReservationParkingSpotStillFree(Reservation reservation)
        {
            var alreadyExistingReservation = UnitOfWork.Reservations.GetReservation(
                reservation.ParkingSpot, reservation.ReservationDate);

            return alreadyExistingReservation == null;
        }
    }
}
