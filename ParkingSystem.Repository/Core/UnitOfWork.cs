﻿using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Repository.Repositories;

namespace ParkingSystem.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ParkingSystemDbContext _context;

        public UnitOfWork(ParkingSystemDbContext context)
        {
            _context = context;
            ParkingSpots = new ParkingSpotRepository(_context);
            Reservations = new ReservationRepository(_context);
            DeletedReservations = new DeletedReservationRepository(_context);
        }

        public IParkingSpotRepository ParkingSpots { get; private set; }
        public IReservationRepository Reservations { get; private set; }
        public IDeletedReservationRepository DeletedReservations { get; private set; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
