using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;

namespace ParkingSystem.Core.Models
{
    public class PagedReservations
    {
        public IList<Reservation> CurrentReservations { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
