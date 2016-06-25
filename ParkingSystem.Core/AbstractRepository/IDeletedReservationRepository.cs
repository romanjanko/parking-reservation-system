using ParkingSystem.DomainModel.Models;
using System;
using System.Collections.Generic;

namespace ParkingSystem.Core.AbstractRepository
{
    public interface IDeletedReservationRepository : IRepository<DeletedReservation>
    {
        IList<DeletedReservation> GetDeletedReservations(ApplicationUser user, DateTime reservationDate, 
            DateTime fromDeletedDate, DateTime toDeletedDate);
    }
}
