using ParkingSystem.DomainModel.Models;

namespace ParkingSystem.Core.ReservationRules
{
    public interface IReservationRulesValidator
    {
        ReservationValidationResult Validate(Reservation reservation);
    }
}
