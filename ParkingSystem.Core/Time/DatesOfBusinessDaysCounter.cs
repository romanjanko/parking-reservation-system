using System;
using ParkingSystem.Core.Time.Convertors;

namespace ParkingSystem.Core.Time
{
    public interface IDatesOfBusinessDaysCounter
    {
        DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear);
        DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(DateTime dateOfDayInWeek);
    }

    public class DatesOfBusinessDaysCounter : IDatesOfBusinessDaysCounter
    {
        private readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly IWeekOfYearToDateConvertor _weekOfYearToDateConvertor;

        public DatesOfBusinessDaysCounter(IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                          IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
        {
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
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

        public DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(DateTime dateOfDayInWeek)
        {
            var weekOfYear = _dateToWeekOfYearConvertor.GetWeekOfYear(dateOfDayInWeek);
            return GetDatesOfBusinessDaysInWeek(weekOfYear);
        }
    }
}