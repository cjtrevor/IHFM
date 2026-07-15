using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public class DailyCareSearchService
    {
        private readonly Vault vault;
        private readonly Configuration configuration;

        public DailyCareSearchService(Vault vault, Configuration configuration)
        {
            this.vault = vault;
            this.configuration = configuration;
        }

        public ObjVerEx GetDailyCareByResidentAndShift(int residentId, string shift, MFIdentifier classToCheck)
        {
            DailyCareLogger.Log($"DailyCareSearchService.GetDailyCareByResidentAndShift START — resident={residentId}, shift={shift}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            MFSearchBuilder search = new MFSearchBuilder(vault);
            search.Class(classToCheck);
            search.Property(configuration.Shift, MFDataType.MFDatatypeText, shift);
            search.Property(configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            DailyCareLogger.Log("DailyCareSearchService.GetDailyCareByResidentAndShift — executing search (FindEx)");
            var results = search.FindEx();
            DailyCareLogger.Log($"DailyCareSearchService.GetDailyCareByResidentAndShift — found={results.Count}");

            ObjVerEx result = null;
            if (results.Count > 1)
                result = search.FindOneEx();

            sw.Stop();
            DailyCareLogger.Log($"DailyCareSearchService.GetDailyCareByResidentAndShift END — elapsed={sw.ElapsedMilliseconds}ms");
            return result;
        }
    }
}
