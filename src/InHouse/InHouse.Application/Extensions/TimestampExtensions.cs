using System;
using System.Collections.Generic;
using System.Text;

namespace InHouse.Application.Extensions
{
    public static class TimestampExtension
    {
        public static string Get12HourClockTime(this int unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var dt = epoch.AddSeconds(unixTime);
            if(dt.Minute > 0 && dt.Second > 0)
                return dt.ToString("h:mm:ss tt");
            if (dt.Minute >0)
                return dt.ToString("h:mm tt");
            else
                return dt.ToString("h tt");
        }
    }
}
