using System;

namespace ParkingSystem.Core.AbstractRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IParkingSpotRepository ParkingSpots { get; }
        IReservationRepository Reservations { get; }
        IDeletedReservationRepository DeletedReservations { get; }

        int SaveChanges();
    }
}
