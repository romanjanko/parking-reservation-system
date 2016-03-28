using ParkingSystem.Core.ReservationRules;
using ParkingSystem.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Pagination;

namespace ParkingSystem.Core.Services
{
    public interface IReservationService
    {
        Reservation GetReservation(int id);
        IList<Reservation> GetAllReservationsForDateRange(IList<DateTime> dateRange);
        IList<Reservation> GetAllReservationsForUser(ApplicationUser user, DateTime fromDate);
        PagedReservations GetAllReservationsForUser(PagingInfo pagination, ApplicationUser user, DateTime fromDate);

        ReservationValidationResult AddReservation(Reservation reservation);
        void DeleteReservation(int id);
    }

    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRulesValidator _reservationRulesValidator;

        public ReservationService(IUnitOfWork unitOfWork, IReservationRulesValidator reservationRulesValidator)
        {
            _unitOfWork = unitOfWork;
            _reservationRulesValidator = reservationRulesValidator;
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
            
            if (validationResult.Success)
            {
                _unitOfWork.Reservations.Add(reservation);
                _unitOfWork.SaveChanges();
            }

            return validationResult;
        }

        public void DeleteReservation(int id)
        {
            var reservation = _unitOfWork.Reservations.Get(id);

            if (reservation != null)
            {
                _unitOfWork.Reservations.Remove(reservation);
                _unitOfWork.SaveChanges();
            }
        }
    }
}
