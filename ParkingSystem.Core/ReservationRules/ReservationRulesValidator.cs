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

            var firstValidationError = validationResults.FirstOrDefault(r => !r.Valid);

            if (firstValidationError != null)
                return firstValidationError;
            
            //TODO
            if (reservation.ParkingSpot.Type == ParkingSpotType.Garage)
            {
                var garageReservedBeforeNoonThreshold = validationResults.FirstOrDefault(r => r.Valid && !r.FreeReservation);
                var garageReservedAfterNoonThreshold = validationResults.FirstOrDefault(r => r.Valid && r.FreeReservation);

                if (garageReservedBeforeNoonThreshold != null)
                    return new SuccessfullGarageReservationBeforeLimitExpiration();
                else
                    return new SuccessfullGarageReservationAfterLimitExpiration();
            }
            else
            {
                return new SuccessfullCommonReservation();
            }
        }
    }
}
