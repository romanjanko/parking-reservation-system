using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;
using ParkingSystem.Core.AbstractRepository;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.ReservationRules.AntiCheatingPolicies;

namespace ParkingSystem.Core.ReservationRules.Definitions
{
    public class ReservationRulesForParkingSpot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;
        private readonly ICurrentTime _currentTime;
        private readonly ICheatingCheck _cheatingCheck;

        public ReservationRulesForParkingSpot(IUnitOfWork unitOfWork,
                                              IDatesOfBusinessDaysCounter datesOfBusinessDaysCounter,
                                              ICurrentTime currentTime,
                                              ICheatingCheck cheatingCheck)
        {
            _unitOfWork = unitOfWork;
            _datesOfBusinessDaysCounter = datesOfBusinessDaysCounter;
            _currentTime = currentTime;
            _cheatingCheck = cheatingCheck;
        }

        public IList<IReservationRule> GetReservationRulesForParkingSpot(ParkingSpot parkingSpot)
        {
            return parkingSpot.Type == ParkingSpotType.Garage ? 
                GetReservationRulesForGarageParkingSpot() : GetReservationRulesForOutsideParkingSpot();
        }

        private IList<IReservationRule> GetReservationRulesForGarageParkingSpot()
        {
            var result = GetReservationRulesSameForAllTypeOfParkingSpots();

            result.Add(new GarageMaxTwiceWeekReservationRule(_unitOfWork, _datesOfBusinessDaysCounter, _cheatingCheck));
            result.Add(new GarageOnMondayOrFridayReservationRule(_unitOfWork, _datesOfBusinessDaysCounter, _cheatingCheck));

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
