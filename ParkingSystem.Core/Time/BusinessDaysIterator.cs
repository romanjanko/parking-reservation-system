using ParkingSystem.Core.Time.Utils;
using System;

namespace ParkingSystem.Core.Time
{
    public interface IBusinessDaysIterator
    {
        DateTime GetPreviousBusinessDayDate(DateTime currentDate);
        DateTime GetNextBusinessDayDate(DateTime currentDate);
    }

    public class BusinessDaysIterator : IBusinessDaysIterator
    {
        private readonly WeekendDayUtils _weekendDayUtils;

        public BusinessDaysIterator(WeekendDayUtils weekendDayUtils)
        {
            _weekendDayUtils = weekendDayUtils;
        }

        public DateTime GetPreviousBusinessDayDate(DateTime currentDate)
        {
            var previousDate = currentDate.AddDays(-1);

            while (_weekendDayUtils.IsWeekendDay(previousDate))
                previousDate = previousDate.AddDays(-1);

            return previousDate;
        }

        public DateTime GetNextBusinessDayDate(DateTime currentDate)
        {
            var nextDate = currentDate.AddDays(1);

            while (_weekendDayUtils.IsWeekendDay(nextDate))
                nextDate = nextDate.AddDays(1);

            return nextDate;
        }
    }
}
