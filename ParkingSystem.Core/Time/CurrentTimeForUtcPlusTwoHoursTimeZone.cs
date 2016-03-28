using System;

namespace ParkingSystem.Core.Time
{
    public class CurrentTimeForUtcPlusTwoHoursTimeZone : ICurrentTime
    {
        public DateTime Now()
        {
            return DateTime.UtcNow.AddHours(2);
        }
    }
}
