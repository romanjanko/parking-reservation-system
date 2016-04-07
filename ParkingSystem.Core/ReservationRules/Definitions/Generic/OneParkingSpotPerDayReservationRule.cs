using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public class OneParkingSpotPerDayReservationRule : AbstractReservationRule
    {
        public OneParkingSpotPerDayReservationRule(IUnitOfWork unitOfWork)
            : base(unitOfWork, null, null, null)
        {
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
            var userReservations = UnitOfWork.Reservations.GetAllReservationsByUser(
                    reservation.ApplicationUser, reservation.ReservationDate, reservation.ReservationDate);

            return userReservations.FirstOrDefault() != null;
        }
    }
}
