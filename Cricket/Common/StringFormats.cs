using System;

namespace Cricket.Common
{
    public static class StringFormats
    {
        public static string FormatA(this TimeSpan timespan)
        {
            return $"{timespan.Hours.ToString("0")}:" +
                   $"{timespan.Minutes.ToString("00")}:" +
                   $"{timespan.Seconds.ToString("00")}:" +
                   $"{timespan.Milliseconds.ToString("000")}";
        }
    }
}
