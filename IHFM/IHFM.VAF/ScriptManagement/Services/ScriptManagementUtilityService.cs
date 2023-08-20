using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class ScriptManagementUtilityService
    {
        private readonly Configuration configuration;

        public ScriptManagementUtilityService(Configuration configuration)
        {
            this.configuration = configuration;
        }
        public MFIdentifier GetScheduledTimeToCheck(DateTime current)
        {
            DateTime start0200 = DateTime.Today + new TimeSpan(1, 0, 0);
            DateTime end0200 = DateTime.Today + new TimeSpan(3, 0, 0);

            DateTime start0600 = DateTime.Today + new TimeSpan(5, 0, 0);
            DateTime end0600 = DateTime.Today + new TimeSpan(7, 0, 0);

            DateTime start0900 = DateTime.Today + new TimeSpan(7, 0, 0);
            DateTime end0900 = DateTime.Today + new TimeSpan(10, 0, 0);

            DateTime start1200 = DateTime.Today + new TimeSpan(11, 0, 0);
            DateTime end1200 = DateTime.Today + new TimeSpan(14, 0, 0);

            DateTime start1400 = DateTime.Today + new TimeSpan(13, 30, 0);
            DateTime end1400 = DateTime.Today + new TimeSpan(15, 30, 0);

            DateTime start1700 = DateTime.Today + new TimeSpan(16, 30, 0);
            DateTime end1700 = DateTime.Today + new TimeSpan(18, 30, 0);

            DateTime start2000 = DateTime.Today + new TimeSpan(20, 0, 0);
            DateTime end2000 = DateTime.Today + new TimeSpan(22, 0, 0);

            DateTime start2200 = DateTime.Today + new TimeSpan(21, 0, 0);
            DateTime end2200 = DateTime.Today + new TimeSpan(23, 0, 0);

            if (current > start0200 && current < end0200)
                return configuration.GiveMeds0200;
            else if (current > start0600 && current < end0600)
                return configuration.GiveMeds0600;
            else if (current > start0900 && current < end0900)
                return configuration.GiveMeds0900;
            else if (current > start1200 && current < end1200)
                return configuration.GiveMeds1200;
            else if (current > start1400 && current < end1400)
                return configuration.GiveMeds1400;
            else if (current > start1700 && current < end1700)
                return configuration.GiveMeds1700;
            else if (current > start2000 && current < end2000)
                return configuration.GiveMeds2000;
            else if (current > start2200 && current < end2200)
                return configuration.GiveMeds2200;

            return null;
        }

        public int GetScheduledTimeslotNumber(DateTime timeslotTme)
        {
            DateTime start0600 = DateTime.Today + new TimeSpan(5, 0, 0);
            DateTime end0600 = DateTime.Today + new TimeSpan(7, 0, 0);

            DateTime start0900 = DateTime.Today + new TimeSpan(7, 0, 0);
            DateTime end0900 = DateTime.Today + new TimeSpan(10, 0, 0);

            DateTime start1200 = DateTime.Today + new TimeSpan(11, 0, 0);
            DateTime end1200 = DateTime.Today + new TimeSpan(14, 0, 0);

            DateTime start1700 = DateTime.Today + new TimeSpan(16, 30, 0);
            DateTime end1700 = DateTime.Today + new TimeSpan(18, 30, 0);

            DateTime start2000 = DateTime.Today + new TimeSpan(20, 0, 0);
            DateTime end2000 = DateTime.Today + new TimeSpan(22, 0, 0);

            if (timeslotTme > start0600 && timeslotTme < end0600)
                return 6;
            else if (timeslotTme > start0900 && timeslotTme < end0900)
                return 9;
            else if (timeslotTme > start1200 && timeslotTme < end1200)
                return 12;
            else if (timeslotTme > start1700 && timeslotTme < end1700)
                return 17;
            else if (timeslotTme > start2000 && timeslotTme < end2000)
                return 20;

            return 0;
        }
    }
}
