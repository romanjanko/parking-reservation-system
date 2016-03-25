using ParkingSystem.Core.RepositoryAbstraction;
using ParkingSystem.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem.Repository.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private ParkingSystemDbContext _context;

        public UnitOfWork(ParkingSystemDbContext context)
        {
            _context = context;
            ParkingSpots = new ParkingSpotRepository(_context);
            Reservations = new ReservationRepository(_context);
        }

        public IParkingSpotRepository ParkingSpots { get; private set; }
        public IReservationRepository Reservations { get; private set; }

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
