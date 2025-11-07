using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Utilities
{
    public static class DateExtensionMethods
    {
        // South Africa Standard Time - GMT+2
        private static readonly TimeZoneInfo ApplicationTimeZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");

        public static DateTime ToLocalDateTime(this Timestamp timestamp)
        {
            DateTime utcDateTime = timestamp.ToDateTime(DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, ApplicationTimeZone);
        }

        public static Timestamp ToUtcTimestamp(this DateTime localDateTime)
        {
            DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, ApplicationTimeZone);
            return utcDateTime.ToTimestamp(DateTimeKind.Utc);
        }

        public static int QuarterDecStart(this DateTime date)
        {
            switch(date.Month)
            {
                case 12:
                case 1:
                case 2:
                    return 1;
                case 3:
                case 4:
                case 5:
                    return 2;
                case 6:
                case 7:
                case 8:
                    return 3;
                case 9:
                case 10:
                case 11:
                    return 4;
                default:
                    return 0;

            }
        }

        public static string Ordinal(this int number)
        {
            string suffix = String.Empty;

            int ones = number % 10;
            int tens = (int)Math.Floor(number / 10M) % 10;

            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }
            return String.Format("{0}{1}", number, suffix);
        }
    }
}
