using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;

namespace ParkingSystem.Core.Models
{
    public class PagedParkingSpots
    {
        public IList<ParkingSpot> CurrentParkingSpots { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
