using System;
using ParkingSystem.Core.Time;
using ParkingSystem.Core.Time.Utils;

namespace ParkingSystem.Core.Services
{
    public interface ICalendarService
    {
        DateTime GetTodayDate();
        DateTime GetNow();

        WeekOfYear GetCurrentWeekOfYear();
        WeekOfYear GetPreviousWeekInYear(WeekOfYear currentWeekOfYear);
        WeekOfYear GetNextWeekInYear(WeekOfYear currentWeekOfYear);

        DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear);

        bool IsWeekendDay(DateTime date);
    }

    public class CalendarService : ICalendarService
    {
        private readonly IWeekOfYearIterator _weekOfYearIterator;
        private readonly ICurrentTime _currentTime;
        private readonly IDatesOfBusinessDaysCounter _datesOfBusinessDaysCounter;
        private readonly WeekendDayUtils _weekendDayUtils;

        public CalendarService(IWeekOfYearIterator weekOfYearIterator,
                               ICurrentTime currentTime,
                               IDatesOfBusinessDaysCounter datesOfBusinessDaysCounter,
                               WeekendDayUtils weekendDayUtils)
        {
            _weekOfYearIterator = weekOfYearIterator;
            _currentTime = currentTime;
            _datesOfBusinessDaysCounter = datesOfBusinessDaysCounter;
            _weekendDayUtils = weekendDayUtils;
        }

        public DateTime GetTodayDate()
        {
            return _currentTime.Now().Date;
        }

        public DateTime GetNow()
        {
            return _currentTime.Now();
        }

        public WeekOfYear GetCurrentWeekOfYear()
        {
            return _weekOfYearIterator.GetCurrentWeekOfYear();
        }
        
        public WeekOfYear GetPreviousWeekInYear(WeekOfYear currentWeekOfYear)
        {
            return _weekOfYearIterator.GetPreviousWeekInYear(currentWeekOfYear);
        }

        public WeekOfYear GetNextWeekInYear(WeekOfYear currentWeekOfYear)
        {            
            return _weekOfYearIterator.GetNextWeekInYear(currentWeekOfYear);
        }
        
        public DatesOfBusinessDays GetDatesOfBusinessDaysInWeek(WeekOfYear weekOfYear)
        {
            return _datesOfBusinessDaysCounter.GetDatesOfBusinessDaysInWeek(weekOfYear);
        }

        public bool IsWeekendDay(DateTime date)
        {
            return _weekendDayUtils.IsWeekendDay(date);
        }
    }
}