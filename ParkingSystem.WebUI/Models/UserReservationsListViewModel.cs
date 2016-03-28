using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.Pagination;

namespace ParkingSystem.WebUI.Models
{
    public class UserReservationsListViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}