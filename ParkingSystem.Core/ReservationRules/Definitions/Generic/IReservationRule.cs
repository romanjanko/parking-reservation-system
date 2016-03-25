using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules.Definitions.Generic
{
    public interface IReservationRule
    {
        ReservationValidationResult Validate(Reservation reservation);
    }
}
