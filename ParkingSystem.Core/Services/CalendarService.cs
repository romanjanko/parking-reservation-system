using ParkingSystem.Core.Models;
using ParkingSystem.Core.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ParkingSystem.Core.Services
{
    public interface ICalendarService
    {
        DateTime GetTodayDate();
        int GetCurrentYear();
        int GetCurrentWeekInYear();

        bool IsWeekendDay(DateTime date);
        
        void GetPreviousWeekInYear(int currentYear, int currentWeek, out int previousYear, out int previousWeek);
        void GetNextWeekInYear(int currentYear, int currentWeek, out int nextYear, out int nextWeek);

        IList<DateTime> GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear);
    }

    public class CalendarService : ICalendarService
    {
        private readonly Calendar _calendar;
        private readonly IDateToWeekOfYearConvertor _dateToWeekOfYearConvertor;
        private readonly IWeekOfYearToDateConvertor _weekOfYearToDateConvertor;

        private DateTime _currentDate
        {
            get
            {
                return DateTime.Today;
            }
        }

        public CalendarService(IDateToWeekOfYearConvertor dateToWeekOfYearConvertor, 
                               IWeekOfYearToDateConvertor weekOfYearToDateConvertor)
        {
            _calendar = CultureInfo.InvariantCulture.Calendar;
            _dateToWeekOfYearConvertor = dateToWeekOfYearConvertor;
            _weekOfYearToDateConvertor = weekOfYearToDateConvertor;
        }

        public DateTime GetTodayDate()
        {
            return _currentDate;
        }

        public int GetCurrentYear()
        {
            return _currentDate.Year;
        }

        public int GetCurrentWeekInYear()
        {
            int year, week;

            _dateToWeekOfYearConvertor.GetWeekOfYear(_currentDate, out year, out week);

            return week;
        }
         
        public void GetPreviousWeekInYear(
            int currentYear, int currentWeek, out int previousYear, out int previousWeek)
        {
            var mondayOfCurrentWeek = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                                            currentYear, currentWeek, DayOfWeek.Monday);
            var mondayOfPreviousWeek = mondayOfCurrentWeek.AddDays(-7);
            
            _dateToWeekOfYearConvertor.GetWeekOfYear(mondayOfPreviousWeek, out previousYear, out previousWeek);
        }

        public void GetNextWeekInYear(
            int currentYear, int currentWeek, out int nextYear, out int nextWeek)
        {
            var mondayOfCurrentWeek = _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(
                                            currentYear, currentWeek, DayOfWeek.Monday);
            var mondayOfNextWeek = mondayOfCurrentWeek.AddDays(7);

            _dateToWeekOfYearConvertor.GetWeekOfYear(mondayOfNextWeek, out nextYear, out nextWeek);
        }
        
        public IList<DateTime> GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear)
        {
            var result = new List<DateTime>
            {
                _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear.Year, weekOfYear.Week, DayOfWeek.Monday),
                _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear.Year, weekOfYear.Week, DayOfWeek.Tuesday),
                _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear.Year, weekOfYear.Week, DayOfWeek.Wednesday),
                _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear.Year, weekOfYear.Week, DayOfWeek.Thursday),
                _weekOfYearToDateConvertor.GetDateForDayInWeekOfYear(weekOfYear.Year, weekOfYear.Week, DayOfWeek.Friday)
            };

            return result.OrderBy(d => d.Date).ToList();
        }

        public bool IsWeekendDay(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}