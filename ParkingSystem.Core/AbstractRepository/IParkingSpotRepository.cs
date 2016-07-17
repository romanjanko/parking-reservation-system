using ParkingSystem.Core.Models;
using ParkingSystem.Core.Pagination;
using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.AbstractRepository
{
    public interface IParkingSpotRepository : IRepository<ParkingSpot>
    {
        PagedParkingSpots GetAll(PagingInfo pagination);
        ParkingSpotsTotals GetParkingSpotsTotals();
    }
}