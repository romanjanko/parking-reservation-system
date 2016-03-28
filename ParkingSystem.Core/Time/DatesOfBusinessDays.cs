using System;
using System.Collections.Generic;

namespace ParkingSystem.Core.Time
{
    public class DatesOfBusinessDays
    {
        public DateTime Monday { get; set; }
        public DateTime Tuesday { get; set; }
        public DateTime Wednesday { get; set; }
        public DateTime Thursday { get; set; }
        public DateTime Friday { get; set; }

        //TODO
        public DateTime this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1:
                        return Monday;
                    case 2:
                        return Tuesday;
                    case 3:
                        return Wednesday;
                    case 4:
                        return Thursday;
                    case 5:
                        return Friday;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        //TODO
        public IList<DateTime> ToList()
        {
            return new List<DateTime>
            {
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Friday
            };
        }
    }
}
