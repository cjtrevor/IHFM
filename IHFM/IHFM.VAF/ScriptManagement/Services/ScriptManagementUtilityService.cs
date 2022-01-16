﻿using MFiles.VAF.Configuration;
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
            int threshold = configuration.ScriptControlTimeThreshold;
            
            DateTime start0600 = DateTime.Today + new TimeSpan(5, 60 - threshold, 0);
            DateTime end0600 = DateTime.Today + new TimeSpan(6, 59, 0);

            DateTime start0900 = DateTime.Today + new TimeSpan(8, 60 - threshold, 0);
            DateTime end0900 = DateTime.Today + new TimeSpan(9, 59, 0);

            DateTime start1200 = DateTime.Today + new TimeSpan(11, 60 - threshold, 0);
            DateTime end1200 = DateTime.Today + new TimeSpan(12, 59, 0);

            DateTime start1700 = DateTime.Today + new TimeSpan(16, 60 - threshold, 0);
            DateTime end1700 = DateTime.Today + new TimeSpan(17, 59, 0);

            DateTime start2000 = DateTime.Today + new TimeSpan(19, 60 - threshold, 0);
            DateTime end2000 = DateTime.Today + new TimeSpan(20, 59, 0);

            if (current > start0600 && current < end0600)
                return configuration.GiveMeds0600;
            else if (current > start0900 && current < end0900)
                return configuration.GiveMeds0900;
            else if (current > start1200 && current < end1200)
                return configuration.GiveMeds1200;
            else if (current > start1700 && current < end1700)
                return configuration.GiveMeds1700;
            else if (current > start2000 && current < end2000)
                return configuration.GiveMeds2000;

            return null;
        }
    }
}