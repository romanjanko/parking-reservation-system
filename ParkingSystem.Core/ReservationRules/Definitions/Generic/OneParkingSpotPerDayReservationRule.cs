using System.Linq;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.RepositoryAbstraction;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class OneParkingSpotPerDayReservationRule : AbstractReservationRule
    {
        public OneParkingSpotPerDayReservationRule(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override ReservationValidationResult Validate(Reservation reservation)
        {
            if (UserAlreadyHasReservationForSameDay(reservation) &&
               !IsReservationBeingMadeByAdminUser(reservation))
                return new FailedReservationValidationResult("You already have a reservation for the same day.");
            else
                return new SuccessfullReservationValidationResult();
        }

        private bool UserAlreadyHasReservationForSameDay(Reservation reservation)
        {
            var userReservations = _unitOfWork.Reservations.GetAllReservationsByUser(
                    reservation.ApplicationUser, reservation.ReservationDate, reservation.ReservationDate);

            return userReservations.FirstOrDefault() != null;
        }
    }
}
