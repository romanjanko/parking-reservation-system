using ParkingSystem.Core.Models;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;

namespace ParkingSystem.Core.RepositoryAbstraction
{
    public interface IParkingSpotRepository : IRepository<ParkingSpot>
    {
        PagedParkingSpots GetAll(PagingInfo pagination);
    }
}