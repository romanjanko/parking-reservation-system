using System;
using System.Globalization;

namespace ParkingSystem.Core.Time.Convertors
{
    public interface IDateToWeekOfYearConvertor
    {
        WeekOfYear GetWeekOfYear(DateTime date);
    }

    public class DateToWeekOfYearConvertor : IDateToWeekOfYearConvertor
    {
        private readonly Calendar _calendar;
        
        public DateToWeekOfYearConvertor()
        {
            _calendar = CultureInfo.InvariantCulture.Calendar;
        }

        /// <summary>
        /// Returns year and week number for given date. It follows ISO 8601 standard for
        /// obtaining weeks numbers. Week 1 is the 1st week of the year with a Thursday in it.
        /// </summary>
        public WeekOfYear GetWeekOfYear(DateTime date)
        {
            // for more information, see: 
            // https://blogs.msdn.microsoft.com/shawnste/2006/01/24/iso-8601-week-of-year-format-in-microsoft-net/
            var day = _calendar.GetDayOfWeek(date);
            var dateForDayGreaterThanWednesday = new DateTime(date.Year, date.Month, date.Day);

            // if its Monday, Tuesday or Wednesday, then it'll 
            // be the same week number as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                dateForDayGreaterThanWednesday = date.AddDays(3);
            }

            // week of our adjusted day
            var week = _calendar.GetWeekOfYear(dateForDayGreaterThanWednesday, CalendarWeekRule.FirstFourDayWeek, 
                DayOfWeek.Monday);

            // last week can have number 52 or 53
            int year;
            if (week >= 52 && dateForDayGreaterThanWednesday.Month == 1)
                year = _calendar.GetYear(dateForDayGreaterThanWednesday) - 1;
            else
                year = _calendar.GetYear(dateForDayGreaterThanWednesday);

            return new WeekOfYear
            {
                Week = week,
                Year = year
            };
        }
    }
}
