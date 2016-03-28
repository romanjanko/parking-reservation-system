using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.Pagination;

namespace ParkingSystem.WebUI.Models
{
    public class ParkingSpotsListViewModel
    {
        public IEnumerable<ParkingSpot> ParkingSpots { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}