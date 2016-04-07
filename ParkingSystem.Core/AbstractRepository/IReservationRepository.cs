using System;
using System.Collections.Generic;
using ParkingSystem.Core.Pagination;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.AbstractRepository
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IList<Reservation> GetAllReservationsForDateRange(DateTime fromDate, DateTime toDate);

        IList<Reservation> GetAllReservationsByUser(ApplicationUser user, DateTime fromDate);
        PagedReservations GetAllReservationsByUser(PagingInfo pagination, ApplicationUser user, DateTime fromDate);
        IList<Reservation> GetAllReservationsByUser(ApplicationUser user, DateTime fromDate, DateTime toDate);

        IList<Reservation> GetNonFreeGarageReservationsByUser(ApplicationUser user, DateTime fromDate, DateTime toDate);

        Reservation GetReservation(ParkingSpot parkingSpot, DateTime date);
    }
}
