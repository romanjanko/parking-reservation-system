using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem.Core.ReservationRules.AntiCheatingPolicies
{
    public interface ICheatingCheck
    {
        void Log(Reservation reservationToBeDeleted, ApplicationUser deletedByUser);
        CheatingCheckResult CheckForCheating(Reservation reservationToBeCreated);
    }

    public class CheatingCheck : ICheatingCheck
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentTime _currentTime;
        private readonly IBusinessDaysIterator _businessDaysIterator;

        public CheatingCheck(IUnitOfWork unitOfWork,
                             ICurrentTime currentTime,
                             IBusinessDaysIterator businessDaysIterator)
        {
            _unitOfWork = unitOfWork;
            _currentTime = currentTime;
            _businessDaysIterator = businessDaysIterator;
        }

        public void Log(Reservation reservationToBeDeleted, ApplicationUser deletedByUser)
        {
            if (deletedByUser.IsUserAdmin())
                return;

            if (IsReservationForTodayOrNextBusinessDay(reservationToBeDeleted) &&
                IsReservationToGarage(reservationToBeDeleted))
            {
                var deletedReservation = new DeletedReservation
                {
                    ApplicationUser = reservationToBeDeleted.ApplicationUser,
                    ParkingSpot = reservationToBeDeleted.ParkingSpot,
                    ReservationDate = reservationToBeDeleted.ReservationDate,
                    ReservedFreely = reservationToBeDeleted.ReservedFreely,
                    DeletedDate = _currentTime.Now().Date
                };

                _unitOfWork.DeletedReservations.Add(deletedReservation);
                _unitOfWork.SaveChanges();
            }
        }

        private bool IsReservationForTodayOrNextBusinessDay(Reservation reservation)
        {
            var todayDate = _currentTime.Now().Date;
            var nextBusinessDayDate = _businessDaysIterator.GetNextBusinessDayDate(todayDate);
            
            return reservation.ReservationDate >= todayDate &&
                   reservation.ReservationDate <= nextBusinessDayDate;
        }

        private bool IsReservationToGarage(Reservation reservation)
        {
            return reservation.ParkingSpot.Type == ParkingSpotType.Garage;
        }

        public CheatingCheckResult CheckForCheating(Reservation reservationToBeCreated)
        {
            if (reservationToBeCreated.ApplicationUser.IsUserAdmin())
                return new NoCheatingDetected();

            var userDeletedReservations = GetDeletedReservationsForReservationDateAndBusinessDayBefore(reservationToBeCreated);

            if (userDeletedReservations.Count() == 0 ||
                userDeletedReservations.Any(r => r.ReservedFreely))
                return new NoCheatingDetected();

            return new PossibleCheatingDetected();
        }

        private IList<DeletedReservation> GetDeletedReservationsForReservationDateAndBusinessDayBefore(Reservation reservation)
        {
            var reservationDate = reservation.ReservationDate.Date;
            var businessDayDateBeforeReservationDate = _businessDaysIterator.GetPreviousBusinessDayDate(reservationDate);

            return _unitOfWork.DeletedReservations.GetDeletedReservations(reservation.ApplicationUser, reservationDate, 
                fromDeletedDate: businessDayDateBeforeReservationDate, toDeletedDate: reservationDate);
        }
    }
}
