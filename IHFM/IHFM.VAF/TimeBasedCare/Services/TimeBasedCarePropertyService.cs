using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class TimeBasedCarePropertyService
    {
        private Vault _vault;
        private Configuration _configuration;
        public TimeBasedCarePropertyService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public int GetAverageTime(Lookup tbcLookup)
        {
            ObjVerEx objVerEx = new ObjVerEx(_vault, tbcLookup);

            int time;

            if (!Int32.TryParse(objVerEx.GetPropertyText(_configuration.AverageTime), out time))
                return 0;

            return time;
        }

        public decimal GetAverageCost(Lookup tbcLookup)
        {
            ObjVerEx objVerEx = new ObjVerEx(_vault, tbcLookup);

            decimal cost;

            if (!Decimal.TryParse(objVerEx.GetPropertyText(_configuration.AverageCost), out cost))
                return 0;

            return cost;
        }
    }
}
