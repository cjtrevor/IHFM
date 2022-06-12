using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Utilities
{
    public static class DateExtensionMethods
    {
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
    }
}
