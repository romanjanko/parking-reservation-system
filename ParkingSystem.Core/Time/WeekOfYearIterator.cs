using System;
using ParkingSystem.Core.Time.Convertors;

namespace ParkingSystem.Core.Time
{
    public interface IWeekOfYearIterator
    {
        WeekOfYear GetCurrentWeekOfYear();
        WeekOfYear GetPreviousWeekInYear(WeekOfYear currentWeekOfYear);
        WeekOfYear GetNextWeekInYear(WeekOfYear currentWeekOfYear);
    }

    public class WeekOfYearIterator : IWeekOfYearIterator
    {
        private readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly IWeekOfYearToDateConvertor _weekOfYearToDateConvertor;
        private readonly ICurrentTime _currentTime;

        public WeekOfYearIterator(IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                  IWeekOfYearToDateConvertor weekOfYearToDateConvertor,
                                  ICurrentTime currentTime)
        {
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            _weekOfYearToDateConvertor = weekOfYearToDateConvertor;
            _currentTime = currentTime;
        }

        public WeekOfYear GetCurrentWeekOfYear()
        {
            return _dateToWeekOfYearConvertor.GetWeekOfYear(_currentTime.Now());
        }

        public WeekOfYear GetPreviousWeekInYear(WeekOfYear currentWeekOfYear)
        {
            var mondayOfCurrentWeek = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                currentWeekOfYear, DayOfWeek.Monday);

            var mondayOfPreviousWeek = mondayOfCurrentWeek.AddDays(-7);

            return _dateToWeekOfYearConvertor.GetWeekOfYear(mondayOfPreviousWeek);
        }

        public WeekOfYear GetNextWeekInYear(WeekOfYear currentWeekOfYear)
        {
            var mondayOfCurrentWeek = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                currentWeekOfYear, DayOfWeek.Monday);

            var mondayOfNextWeek = mondayOfCurrentWeek.AddDays(7);

            return _dateToWeekOfYearConvertor.GetWeekOfYear(mondayOfNextWeek);
        }
    }
}
