using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;

namespace ParkingSystem.Core.ReservationRules.Definitions
{
    public class ReservationRulesForParkingSpot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;
        private readonly ICurrentTime _currentTime;

        public ReservationRulesForParkingSpot(IUnitOfWork unitOfWork,
                                              IDatesOfBusinessDaysCounter datesOfBusinessDaysCounter,
                                              ICurrentTime currentTime)
        {
            _unitOfWork = unitOfWork;
            _datesOfBusinessDaysCounter = datesOfBusinessDaysCounter;
            _currentTime = currentTime;
        }

        public IList<IReservationRule> GetReservationRulesForParkingSpot(ParkingSpot parkingSpot)
        {
            return parkingSpot.Type == ParkingSpotType.Garage ? 
                GetReservationRulesForGarageParkingSpot() : GetReservationRulesForOutsideParkingSpot();
        }

        private IList<IReservationRule> GetReservationRulesForGarageParkingSpot()
        {
            var result = GetReservationRulesSameForAllTypeOfParkingSpots();

            result.Add(new GarageMaxTwiceWeekReservationRule(_unitOfWork, _datesOfBusinessDaysCounter));
            result.Add(new GarageOnMondayOrFridayReservationRule(_unitOfWork, _datesOfBusinessDaysCounter));

            return result;
        }

        private IList<IReservationRule> GetReservationRulesForOutsideParkingSpot()
        {
            return GetReservationRulesSameForAllTypeOfParkingSpots();
        }

        private IList<IReservationRule> GetReservationRulesSameForAllTypeOfParkingSpots()
        {
            return new List<IReservationRule>()
            {
                new NoPastDatesReservationRule(_currentTime),
                new OneParkingSpotPerDayReservationRule(_unitOfWork),
                new OnlyFreeParkingSpotReservationRule(_unitOfWork)
            };
        }
    }
}
