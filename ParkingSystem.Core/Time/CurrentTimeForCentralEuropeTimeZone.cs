using System;

namespace ParkingSystem.Core.Time
{
    public class CurrentTimeForCentralEuropeTimeZone : ICurrentTime
    {
        private TimeZoneInfo CentralEuropeTimeZoneInfo { get; set; }

        public CurrentTimeForCentralEuropeTimeZone()
        {
            CentralEuropeTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Europe Standard Time");
        }

        public DateTime Now()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, CentralEuropeTimeZoneInfo);
        }
    }
}
