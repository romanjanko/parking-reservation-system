using System;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.Time.Convertors
{
    public interface IWeekOfYearToDateConvertor
    {
        DateTime GetDateForDayInWeekOfYear(WeekOfYear weekOfYear, DayOfWeek day);
    }

    public class WeekOfYearToDateConvertor : IWeekOfYearToDateConvertor
    {
        private readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly DayOfWeekUtils _dayOfWeekUtils;

        public WeekOfYearToDateConvertor(IDateToWeekOfYearConvertor dateToWeekOfYearConvertor,
                                         DayOfWeekUtils dayOfWeekUtils)
        {
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            _dayOfWeekUtils = dayOfWeekUtils;
        }

        public DateTime GetDateForDayInWeekOfYear(WeekOfYear weekOfYear, DayOfWeek day)
        {
            var startOfYear = new DateTime(weekOfYear.Year, 1, 1);
            var tempWeekOfYear = _dateToWeekOfYearConvertor.GetWeekOfYear(startOfYear);

            DateTime mondayOfFirstWeek;
            if (tempWeekOfYear.Week == 1)
            {
                mondayOfFirstWeek = startOfYear.AddDays(- _dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, 
                    startOfYear.DayOfWeek));
            }
            else
            {
                mondayOfFirstWeek = startOfYear.AddDays(7 - _dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday,
                    startOfYear.DayOfWeek));
            }

            var daysOffsetInsideWeek = _dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, day);

            return mondayOfFirstWeek.AddDays(7 * (weekOfYear.Week - 1) + daysOffsetInsideWeek);
        }
    }
}
