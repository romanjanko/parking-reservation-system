using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class OnlyFreeParkingSpotReservationRule : AbstractReservationRule
    {
        private readonly IUnitOfWork _unitOfWork;

        public OnlyFreeParkingSpotReservationRule(IUnitOfWork unitOfWork)
            : base()
        {
            _unitOfWork = unitOfWork;
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (IsReservationParkingSpotStillFree(reservation))
                return new SuccessfullCommonReservation();
            else
                return new FailedReservation("The parking spot is already reserved for this day.");
        }

        private bool IsReservationParkingSpotStillFree(Reservation reservation)
        {
            var alreadyExistingReservation = _unitOfWork.Reservations.GetReservation(
                reservation.ParkingSpot, reservation.ReservationDate);

            return alreadyExistingReservation == null;
        }
    }
}
