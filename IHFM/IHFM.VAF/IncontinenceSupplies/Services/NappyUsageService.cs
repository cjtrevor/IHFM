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
            ObjVerEx currentCount = FindCurrentMonthlyNappyRecord(residentId);

            if(currentCount == null)
            {
                CreateNewMonthlyNappyRecord(siteId, residentId);
                return;
            }

            UpdateCurrentUsage(currentCount);
        }

        private void UpdateCurrentUsage(ObjVerEx currentUsage)
        {
            int currentTotal = currentUsage.GetProperty(_configuration.NappyUsage_TotalMonthlyUsage).GetValue<int>();
            currentUsage.SaveProperty(_configuration.NappyUsage_TotalMonthlyUsage,MFDataType.MFDatatypeInteger, currentTotal + 1);
        }

        private ObjVerEx FindCurrentMonthlyNappyRecord(int residentId)
        {
            MFSearchBuilder nappySearch = new MFSearchBuilder(_vault);
            nappySearch.Class(_configuration.NappyUsage_MonthlyCountClass);
            nappySearch.Deleted(false);
            nappySearch.Property(_configuration.NappyUsage_Month, MFDataType.MFDatatypeText, DateTime.Today.ToString("MMMM"));
            nappySearch.Property(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId);

            if (nappySearch.FindEx().Count == 0)
                return null;

            return nappySearch.FindOneEx();
        }

        private void CreateNewMonthlyNappyRecord(int siteId, int residentId)
        {
            MFPropertyValuesBuilder builder = new MFPropertyValuesBuilder(_vault);
            builder.SetClass(_configuration.NappyUsage_MonthlyCountClass)
                .Add(_configuration.VAFSite, MFDataType.MFDatatypeLookup, siteId)
                .SetLookup(_configuration.ResidentLookup, residentId)
                .Add(_configuration.NappyUsage_Month, MFDataType.MFDatatypeText, DateTime.Today.ToString("MMMM"))
                .Add(_configuration.NappyUsage_TotalMonthlyUsage, MFDataType.MFDatatypeInteger, 1);
            _vault.ObjectOperations.CreateNewObjectExQuick(_configuration.NappyUsage_MonthlyUsageObject.ID, builder.Values);
        }
    }
}
