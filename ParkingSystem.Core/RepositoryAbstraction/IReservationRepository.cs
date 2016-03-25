using ParkingSystem.Core.Models;
using ParkingSystem.DomainModel.Models;
using System;
using System.Collections.Generic;

namespace ParkingSystem.Core.RepositoryAbstraction
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        IList<Reservation> GetAllReservationsForDateRange(DateTime fromDate, DateTime toDate);

        IList<Reservation> GetAllReservationsByUser(ApplicationUser user, DateTime fromDate);
        PagedReservations GetAllReservationsByUser(PagingInfo pagination, ApplicationUser user, DateTime fromDate);
        IList<Reservation> GetAllReservationsByUser(ApplicationUser user, DateTime fromDate, DateTime toDate);

        IList<Reservation> GetGarageReservationsByUser(ApplicationUser user, DateTime fromDate, DateTime toDate);

        Reservation GetReservation(ParkingSpot parkingSpot, DateTime date);
    }
}
