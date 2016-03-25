using System.Collections.Generic;
using System.Linq;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.Core.ReservationRules.Definitions;

namespace ParkingSystem.Core.ReservationRules
{
    public class ReservationRulesValidator : IReservationRulesValidator
    {
        private readonly ReservationRulesForParkingSpot _reservationRulesForParkingSpot;

        public ReservationRulesValidator(ReservationRulesForParkingSpot reservationRulesForParkingSpot)
        {
            _reservationRulesForParkingSpot = reservationRulesForParkingSpot;
        }

        public ReservationValidationResult Validate(Reservation reservation)
        {
            var rules = _reservationRulesForParkingSpot.GetReservationRulesForParkingSpot(reservation.ParkingSpot);

            var validationResults = new List<ReservationValidationResult>();
            foreach (var rule in rules)
                validationResults.Add(rule.Validate(reservation));

            var firstValidationError = validationResults.Where(r => r.Success == false).FirstOrDefault();

            if (firstValidationError != null)
            {
                return firstValidationError;
            }
            else
            {
                return new SuccessfullReservationValidationResult();
            }
        }
    }
}
