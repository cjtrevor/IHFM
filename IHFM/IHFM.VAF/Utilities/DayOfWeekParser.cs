using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public static class DayOfWeekParser
    {
        public static bool isToday(string dayName)
        {
            return dayName == DateTime.Now.DayOfWeek.ToString();
        }
    }
}
