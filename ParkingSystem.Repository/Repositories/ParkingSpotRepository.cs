using System.Collections.Generic;
using System.Linq;
using ParkingSystem.Repository.Core;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.RepositoryAbstraction;
using ParkingSystem.Core.Models;
using System;

namespace ParkingSystem.Repository.Repositories
{
    public class ParkingSpotRepository : Repository<ParkingSpot>, IParkingSpotRepository
    {
        public ParkingSpotRepository(ParkingSystemDbContext context)
            : base(context)
        {
        }

        public ParkingSystemDbContext ParkingSystemContext
        {
            get { return _context as ParkingSystemDbContext; }
        }

        public PagedParkingSpots GetAll(PagingInfo pagination)
        {
            if (pagination.CurrentPage <= 0 || pagination.ItemsPerPage <= 0)
                throw new InvalidOperationException("Invalid pagination.");

            var parkingSpots = ParkingSystemContext.ParkingSpots;

            var parkingSpotsForCurrentPage = parkingSpots
                .OrderBy(x => x.Type)
                .ThenBy(x => x.Name)
                .Skip((pagination.CurrentPage - 1) * pagination.ItemsPerPage)
                .Take(pagination.ItemsPerPage)
                .ToList();

            var totalparkingSpots = parkingSpots.Count();

            return new PagedParkingSpots
            {
                CurrentParkingSpots = parkingSpotsForCurrentPage,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = pagination.CurrentPage,
                    ItemsPerPage = pagination.ItemsPerPage,
                    TotalItems = totalparkingSpots
                }
            };
        }
    }
}
