using System;

namespace ParkingSystem.Core.Utils
{
    public class DayOfWeekUtils
    {
        public int GetDaysOffset(DayOfWeek dayOfWeek1, DayOfWeek dayOfWeek2)
        {
            int day1 = DayOfWeekToInt(dayOfWeek1);
            int day2 = DayOfWeekToInt(dayOfWeek2);

            return day2 - day1;
        }

        public int DayOfWeekToInt(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Sunday)
                return 7;
            else
                return (int)dayOfWeek;
        }
    }
}
