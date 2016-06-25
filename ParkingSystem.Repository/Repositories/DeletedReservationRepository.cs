using System;
using System.Collections.Generic;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Repository.Core;
using System.Linq;

namespace ParkingSystem.Repository.Repositories
{
    public class DeletedReservationRepository : Repository<DeletedReservation>, IDeletedReservationRepository
    {
        public DeletedReservationRepository(ParkingSystemDbContext context)
            : base(context)
        {
        }

        public ParkingSystemDbContext ParkingSystemContext
        {
            get { return _context as ParkingSystemDbContext; }
        }

        public IList<DeletedReservation> GetDeletedReservations(ApplicationUser user, DateTime reservationDate, 
            DateTime fromDeletedDate, DateTime toDeletedDate)
        {
            return ParkingSystemContext.DeletedReservations
                .Where(r => r.ReservationDate == reservationDate &&
                            r.DeletedDate >= fromDeletedDate && 
                            r.DeletedDate <= toDeletedDate &&
                            r.ApplicationUser.Id == user.Id &&
                            r.ParkingSpot.Type == ParkingSpotType.Garage).ToList();
        }
    }
}
