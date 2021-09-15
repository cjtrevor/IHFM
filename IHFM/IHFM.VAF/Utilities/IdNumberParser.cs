using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            return int.Parse(DateTime.Now.Day.ToString().PadLeft(2, '0'));
        }

        public static int GetCurrentMonthPart()
        {
            return int.Parse(DateTime.Now.Month.ToString().PadLeft(2, '0'));
        }

        public static int GetCurrentYearPart()
        {
            return int.Parse(DateTime.Now.Year.ToString().Substring(2, 2));
        }

        public static bool ValidateIDNumber(string idNumber)
        {
            if (string.IsNullOrEmpty(idNumber) && idNumber.Length != 13)
                return false;

            if (!Regex.IsMatch(idNumber, @"(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))"))
                return false;

            return true;
        }

    }
}
