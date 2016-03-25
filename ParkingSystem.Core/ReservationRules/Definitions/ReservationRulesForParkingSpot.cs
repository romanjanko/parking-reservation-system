using ParkingSystem.Core.RepositoryAbstraction;
using ParkingSystem.Core.ReservationRules.Definitions.Generic;
using ParkingSystem.Core.Utils;
using ParkingSystem.DomainModel.Models;
using System.Collections.Generic;

namespace ParkingSystem.Core.ReservationRules.Definitions
{
    public class ReservationRulesForParkingSpot
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly IWeekOfYearToDateConvertor _weekOfYearToDateConvertor;

        public ReservationRulesForParkingSpot(IUnitOfWork unitOfWork,
                                              IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                              IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
        {
            _unitOfWork = unitOfWork;
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            _weekOfYearToDateConvertor = weekOfYearToDateConvertor;
        }

        public IList<IReservationRule> GetReservationRulesForParkingSpot(ParkingSpot parkingSpot)
        {
            if (parkingSpot.Type == ParkingSpotType.Garage)
                return GetReservationRulesForGarageParkingSpot();
            else
                return GetReservationRulesForOutsideParkingSpot();
        }

        private IList<IReservationRule> GetReservationRulesForGarageParkingSpot()
        {
            var result = GetReservationRulesSameForAllTypeOfParkingSpots();

            result.Add(new GarageMaxTwiceWeekReservationRule(
                                _unitOfWork, _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor));
            result.Add(new GarageOnMondayOrFridayReservationRule(
                                _unitOfWork, _dateToWeekOfYearConvertor, _weekOfYearToDateConvertor));

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
                new NoPastDatesReservationRule(),
                new OneParkingSpotPerDayReservationRule(_unitOfWork),
                new OnlyFreeParkingSpotReservationRule(_unitOfWork)
            };
        }
    }
}
