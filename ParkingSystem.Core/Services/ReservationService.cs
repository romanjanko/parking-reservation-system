using ParkingSystem.Core.ReservationRules;
using ParkingSystem.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Pagination;
using ParkingSystem.Core.Time;

namespace ParkingSystem.Core.Services
{
    public interface IReservationService
    {
        Reservation GetReservation(int id);
        IList<Reservation> GetAllReservationsForDateRange(IList<DateTime> dateRange);
        IList<Reservation> GetAllReservationsForUser(ApplicationUser user);
        IList<Reservation> GetAllReservationsForUser(ApplicationUser user, DateTime fromDate);
        PagedReservations GetAllReservationsForUser(PagingInfo pagination, ApplicationUser user, DateTime fromDate);

        ReservationValidationResult AddReservation(Reservation reservation);

        bool CanBeReservationDeletedByUser(ApplicationUser user, Reservation reservation);
        void DeleteReservation(int id);
        void DeleteAllReservationsForUser(ApplicationUser user);
    }

    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRulesValidator _reservationRulesValidator;
        private readonly ICurrentTime _currentTime;

        public ReservationService(IUnitOfWork unitOfWork, 
                                  IReservationRulesValidator reservationRulesValidator, 
                                  ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _reservationRulesValidator = reservationRulesValidator;
            _currentTime = currentTime;
        }

        public Reservation GetReservation(int id)
        {
            return _unitOfWork.Reservations.Get(id);
        }

        public IList<Reservation> GetAllReservationsForDateRange(IList<DateTime> dateRange)
        {
            var fromDate = dateRange.Min();
            var toDate = dateRange.Max();

            return _unitOfWork.Reservations.GetAllReservationsForDateRange(fromDate, toDate);
        }

        public IList<Reservation> GetAllReservationsForUser(ApplicationUser user)
        {
            return _unitOfWork.Reservations.GetAllReservationsByUser(user);
        }

        public IList<Reservation> GetAllReservationsForUser(ApplicationUser user, DateTime fromDate)
        {
            return _unitOfWork.Reservations.GetAllReservationsByUser(user, fromDate);
        }

        public PagedReservations GetAllReservationsForUser(PagingInfo pagination, ApplicationUser user, DateTime fromDate)
        {
            return _unitOfWork.Reservations.GetAllReservationsByUser(pagination, user, fromDate);
        }

        public ReservationValidationResult AddReservation(Reservation reservation)
        {
            var validationResult = _reservationRulesValidator.Validate(reservation);
            
            if (validationResult.Valid)
            {
                if (validationResult.GetType() == typeof(SuccessfullFreeGarageReservation))
                    reservation.ReservedFreely = true;

                _unitOfWork.Reservations.Add(reservation);
                _unitOfWork.SaveChanges();
            }

            return validationResult;
        }

        public bool CanBeReservationDeletedByUser(ApplicationUser user, Reservation reservation)
        {
            if (IsPastReservation(reservation) ||
                IsTodayReservationAfterTimeLimit(reservation))
                return false;

            return IsReservationCreatedByUser(user, reservation) ||
                   user.IsUserAdmin();
        }

        private bool IsPastReservation(Reservation reservation)
        {
            return reservation.ReservationDate.Date < _currentTime.Now().Date;
        }

        private bool IsTodayReservationAfterTimeLimit(Reservation reservation)
        {
            return reservation.ReservationDate.Date == _currentTime.Now().Date &&
                _currentTime.Now().TimeOfDay >= new TimeSpan(10, 0, 0); //10:00AM
        }

        private bool IsReservationCreatedByUser(ApplicationUser user, Reservation reservation)
        {
            return string.Compare(reservation.ApplicationUser.UserName, user.UserName, StringComparison.Ordinal) == 0;
        }

        public void DeleteReservation(int id)
        {
            var reservation = _unitOfWork.Reservations.Get(id);

            if (reservation == null) return;

            _unitOfWork.Reservations.Remove(reservation);
            _unitOfWork.SaveChanges();
        }

        public void DeleteAllReservationsForUser(ApplicationUser user)
        {
            var reservationsForUser = GetAllReservationsForUser(user);

            _unitOfWork.Reservations.RemoveRange(reservationsForUser);
            _unitOfWork.SaveChanges();
        }
    }
}
