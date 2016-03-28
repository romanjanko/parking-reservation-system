using System;
using ParkingSystem.Core.Time.Convertors;

namespace ParkingSystem.Core.Time
{
    public interface IDatesOfBusinessDaysCounter
    {
        DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear);
    }

    public class DatesOfBusinessDaysCounter : IDatesOfBusinessDaysCounter
    {
        private readonly IWeekOfYearToDateConvertor _weekOfYearToDateConvertor;

        public DatesOfBusinessDaysCounter(IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
        {
            _weekOfYearToDateConvertor = weekOfYearToDateConvertor;
        }

        public DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear)
        {
            return new DatesOfBusinessDays
            {
                Monday = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Monday),
                Tuesday = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Tuesday),
                Wednesday = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Wednesday),
                Thursday = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Thursday),
                Friday = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear, DayOfWeek.Friday)
            };
        }
    }
}