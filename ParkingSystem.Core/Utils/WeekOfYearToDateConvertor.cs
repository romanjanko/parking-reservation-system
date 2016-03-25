using System;

namespace ParkingSystem.Core.Utils
{
    public interface IWeekOfYearToDateConvertor
    {
        DateTime GetDateForDayInWeekOfYear(int year, int week, DayOfWeek day);
    }

    public class WeekOfYearToDateConvertor : IWeekOfYearToDateConvertor
    {
        private readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly DayOfWeekUtils _dayOfWeekUtils;

        public WeekOfYearToDateConvertor(IDateToWeekOfYearConvertor dateToWeekOfYearConvertor, DayOfWeekUtils dayOfWeekUtils)
        {
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            _dayOfWeekUtils = dayOfWeekUtils;
        }

        public DateTime GetDateForDayInWeekOfYear(int year, int week, DayOfWeek day)
        {
            DateTime startOfYear = new DateTime(year, 1, 1);
            DateTime mondayOfFirstWeek;
            int daysOffsetInsideWeek, tempYear, tempWeek;

            _dateToWeekOfYearConvertor.GetWeekOfYear(startOfYear, out tempYear, out tempWeek);
            
            if (tempWeek == 1)
            {
                mondayOfFirstWeek = startOfYear.AddDays(-
                    _dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, startOfYear.DayOfWeek));
            }
            else
            {
                mondayOfFirstWeek = startOfYear.AddDays(7 - 
                    _dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, startOfYear.DayOfWeek));
            }

            daysOffsetInsideWeek = _dayOfWeekUtils.GetDaysOffset(DayOfWeek.Monday, day);

            return mondayOfFirstWeek.AddDays(7 * (week - 1) + daysOffsetInsideWeek);
        }
    }
}
