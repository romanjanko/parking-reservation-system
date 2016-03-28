using System.Collections.Generic;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.Pagination
{
    public class PagedReservations
    {
        public IList<Reservation> CurrentReservations { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
