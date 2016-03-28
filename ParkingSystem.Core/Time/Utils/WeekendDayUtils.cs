using System;

namespace ParkingSystem.Core.Time.Utils
{
    public class WeekendDayUtils
    {
        public bool IsWeekendDay(DateTime date)
        {
            return (date.DayOfWeek == DayOfWeek.Saturday ||
                    date.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
