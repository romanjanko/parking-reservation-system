using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class OneParkingSpotPerDayReservationRule : AbstractReservationRule
    {
        private readonly IUnitOfWork _unitOfWork;

        public OneParkingSpotPerDayReservationRule(IUnitOfWork unitOfWork)
            : base()
        {
            _unitOfWork = unitOfWork;
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (UserAlreadyHasReservationForSameDay(reservation) &&
               !IsReservationMadeByAdminUser(reservation))
                return new FailedReservation("You already have a reservation for the same day.");
            else
                return new SuccessfullCommonReservation();
        }

        private bool UserAlreadyHasReservationForSameDay(Reservation reservation)
        {
            var userReservations = _unitOfWork.Reservations.GetAllReservationsByUser(
                    reservation.ApplicationUser, reservation.ReservationDate, reservation.ReservationDate);

            return userReservations.Any();
        }
    }
}
