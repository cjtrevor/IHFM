using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Utilities
{
    public static class IdNumberParser
    {
        public static int GetDayPartFromIDNumber(string idNumber)
        {
            return int.Parse(idNumber.Substring(4, 2));
        }

        public static int GetMonthPartFromIDNumber(string idNumber)
        {
            return int.Parse(idNumber.Substring(2, 2));
        }

        public static int GetYearPartFromIDNumber(string idNumber)
        {
            return int.Parse(idNumber.Substring(0, 2));
        }

         public static int GetCurrentDayPart()
        {
            return Int32.Parse(DateTime.Now.Day.ToString().PadLeft(2, '0'));
        }

        public static int GetCurrentMonthPart()
        {
            return Int32.Parse(DateTime.Now.Month.ToString().PadLeft(2, '0'));
        }

        public static int GetCurrentYearPart()
        {
            return int.Parse(DateTime.Now.Year.ToString().Substring(2, 2));
        }

    }
}
