using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class NappyUsageService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public NappyUsageService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void LogMonthlyNappyUsage(int siteId, int residentId)
        {
            DailyCareLogger.Log($"NappyUsageService.LogMonthlyNappyUsage START — resident={residentId}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            DailyCareLogger.Log("NappyUsageService.LogMonthlyNappyUsage — searching for current monthly record");
            ObjVerEx currentCount = FindCurrentMonthlyNappyRecord(residentId);

            if(currentCount == null)
            {
                DailyCareLogger.Log("NappyUsageService.LogMonthlyNappyUsage — no record found, creating new");
                CreateNewMonthlyNappyRecord(siteId, residentId);
                sw.Stop();
                DailyCareLogger.Log($"NappyUsageService.LogMonthlyNappyUsage END — elapsed={sw.ElapsedMilliseconds}ms");
                return;
            }

            DailyCareLogger.Log("NappyUsageService.LogMonthlyNappyUsage — record found, updating usage");
            UpdateCurrentUsage(currentCount);

            sw.Stop();
            DailyCareLogger.Log($"NappyUsageService.LogMonthlyNappyUsage END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void UpdateCurrentUsage(ObjVerEx currentUsage)
        {
            DailyCareLogger.Log("NappyUsageService.UpdateCurrentUsage START");
            var sw = System.Diagnostics.Stopwatch.StartNew();
            int currentTotal = currentUsage.GetProperty(_configuration.NappyUsage_TotalMonthlyUsage).GetValue<int>();
            currentUsage.SaveProperty(_configuration.NappyUsage_TotalMonthlyUsage,MFDataType.MFDatatypeInteger, currentTotal + 1);
            sw.Stop();
            DailyCareLogger.Log($"NappyUsageService.UpdateCurrentUsage END — newTotal={currentTotal + 1}, elapsed={sw.ElapsedMilliseconds}ms");
        }

        private ObjVerEx FindCurrentMonthlyNappyRecord(int residentId)
        {
            DailyCareLogger.Log($"NappyUsageService.FindCurrentMonthlyNappyRecord START — resident={residentId}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            MFSearchBuilder nappySearch = new MFSearchBuilder(_vault);
            nappySearch.Class(_configuration.NappyUsage_MonthlyCountClass);
            nappySearch.Deleted(false);
            nappySearch.Property(_configuration.NappyUsage_Month, MFDataType.MFDatatypeText, DateTime.Today.ToString("MMMM"));
            nappySearch.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            var results = nappySearch.FindEx();

            sw.Stop();
            DailyCareLogger.Log($"NappyUsageService.FindCurrentMonthlyNappyRecord END — found={results.Count > 0}, elapsed={sw.ElapsedMilliseconds}ms");

            if (results.Count == 0)
                return null;

            return nappySearch.FindOneEx();
        }

        private void CreateNewMonthlyNappyRecord(int siteId, int residentId)
        {
            DailyCareLogger.Log($"NappyUsageService.CreateNewMonthlyNappyRecord START — resident={residentId}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            MFPropertyValuesBuilder builder = new MFPropertyValuesBuilder(_vault);
            builder.SetClass(_configuration.NappyUsage_MonthlyCountClass)
                .Add(_configuration.VAFSite, MFDataType.MFDatatypeLookup, siteId)
                .SetLookup(_configuration.ResidentLookup, residentId)
                .Add(_configuration.NappyUsage_Month, MFDataType.MFDatatypeText, DateTime.Today.ToString("MMMM"))
                .Add(_configuration.NappyUsage_TotalMonthlyUsage, MFDataType.MFDatatypeInteger, 1);
            _vault.ObjectOperations.CreateNewObjectExQuick(_configuration.NappyUsage_MonthlyUsageObject.ID, builder.Values);

            sw.Stop();
            DailyCareLogger.Log($"NappyUsageService.CreateNewMonthlyNappyRecord END — elapsed={sw.ElapsedMilliseconds}ms");
        }
    }
}
