using System.Collections.Generic;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.Pagination
{
    public class PagedParkingSpots
    {
        public IList<ParkingSpot> CurrentParkingSpots { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
