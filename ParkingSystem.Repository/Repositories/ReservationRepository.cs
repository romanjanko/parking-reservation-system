using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Repository.Core;
using ParkingSystem.DomainModel.Models;
using System;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Pagination;

namespace ParkingSystem.Repository.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ParkingSystemDbContext context)
            : base(context)
        {
        }

        public ParkingSystemDbContext ParkingSystemContext
        {
            get { return _context as ParkingSystemDbContext; }
        }
        
        public IList<Reservation> GetAllReservationsForDateRange(DateTime fromDate, DateTime toDate)
        {
            return ParkingSystemContext.Reservations
                .Where(r => r.ReservationDate >= fromDate && r.ReservationDate <= toDate).ToList();
        }

        public IList<Reservation> GetAllReservationsByUser(ApplicationUser user, DateTime fromDate)
        {
            return ParkingSystemContext.Reservations
                .Where(r => r.ReservationDate >= fromDate && r.ApplicationUser.Id == user.Id).ToList();
        }

        public PagedReservations GetAllReservationsByUser(PagingInfo pagination, ApplicationUser user, DateTime fromDate)
        {
            if (pagination.CurrentPage <= 0 || pagination.ItemsPerPage <= 0)
                throw new InvalidOperationException("Invalid pagination.");

            var reservations = ParkingSystemContext.Reservations
                .Where(r => r.ReservationDate >= fromDate && r.ApplicationUser.Id == user.Id);

            var reservationsForCurrentPage = reservations
                .OrderBy(r => r.ReservationDate)
                .Skip((pagination.CurrentPage - 1) * pagination.ItemsPerPage)
                .Take(pagination.ItemsPerPage)
                .ToList();

            var totalReservations = reservations.Count();

            return new PagedReservations
            {
                CurrentReservations = reservationsForCurrentPage,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pagination.CurrentPage,
                    ItemsPerPage = pagination.ItemsPerPage,
                    TotalItems = totalReservations
                }
            };
        }

        public IList<Reservation> GetAllReservationsByUser(ApplicationUser user, DateTime fromDate, DateTime toDate)
        {
            return ParkingSystemContext.Reservations
                .Where(r => r.ReservationDate >= fromDate && r.ReservationDate <= toDate && 
                            r.ApplicationUser.Id == user.Id).ToList();
        }

        public IList<Reservation> GetNonFreeGarageReservationsByUser(ApplicationUser user, DateTime fromDate, DateTime toDate)
        {
            return ParkingSystemContext.Reservations
                .Where(r => r.ReservationDate >= fromDate && r.ReservationDate <= toDate &&
                            r.ReservedFreely == false &&
                            r.ApplicationUser.Id == user.Id &&
                            r.ParkingSpot.Type == ParkingSpotType.Garage).ToList();
        }

        public Reservation GetReservation(ParkingSpot parkingSpot, DateTime date)
        {
            return ParkingSystemContext.Reservations
                .FirstOrDefault(r => r.ParkingSpot.Id == parkingSpot.Id && r.ReservationDate == date);
        }
    }
}
