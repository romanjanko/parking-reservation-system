using System;

namespace ParkingSystem.Core.Time
{
    public class CurrentTimeForUtcPlusTwoHoursTimeZone : ICurrentTime
    {
        public DateTime Now()
        {
            //TODO +2 during summer time, +1 during winter time? check and fix
            return DateTime.UtcNow.AddHours(2);
        }
    }
}
