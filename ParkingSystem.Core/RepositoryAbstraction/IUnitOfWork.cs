using System;

namespace ParkingSystem.Core.RepositoryAbstraction
{
    public interface IUnitOfWork : IDisposable
    {
        IParkingSpotRepository ParkingSpots { get; }
        IReservationRepository Reservations { get; }
        
        int SaveChanges();
    }
}
