using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;

namespace IHFM.VAF
{
    public class VitalsRecordExportService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public VitalsRecordExportService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void ExportRecord(ObjVerEx vitalsRecord)
        {
            DatabaseConnector connector = new DatabaseConnector();
            SiteSearchService searchService = new SiteSearchService(_vault, _configuration);

            string shift = vitalsRecord.GetPropertyText(_configuration.Shift);
            int objectId = vitalsRecord.ObjID.ID;
            int siteId = Int32.Parse(vitalsRecord.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());

            ObjVerEx site = searchService.GetSiteByNumber(siteId.ToString());
            string siteName = site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();

            string resident = vitalsRecord.GetProperty(_configuration.ResidentLookup).GetValueAsLocalizedText();
            DateTime dateTaken = DateTime.Now;
            decimal temperature = vitalsRecord.HasValue(_configuration.Vitals_Temperature) 
                ? GetDecimalValue(vitalsRecord.GetPropertyText(_configuration.Vitals_Temperature)) 
                : 0;
            int systolicBP = vitalsRecord.HasValue(_configuration.Vitals_SystolicBP)
                ? GetIntValue(vitalsRecord.GetPropertyText(_configuration.Vitals_SystolicBP))
                : 0;
            int diastolicBP = vitalsRecord.HasValue(_configuration.Vitals_DiastolicBP)
                ? GetIntValue(vitalsRecord.GetPropertyText(_configuration.Vitals_DiastolicBP))
                : 0;
            int heartRate = vitalsRecord.HasValue(_configuration.Vitals_HeartRate)
                ? GetIntValue(vitalsRecord.GetPropertyText(_configuration.Vitals_HeartRate))
                : 0;
            decimal weight = vitalsRecord.HasValue(_configuration.Vitals_Weight)
                ? GetDecimalValue(vitalsRecord.GetPropertyText(_configuration.Vitals_Weight))
                : 0;
            decimal hgt = vitalsRecord.HasValue(_configuration.Vitals_HGT)
                ? GetDecimalValue(vitalsRecord.GetPropertyText(_configuration.Vitals_HGT))
                : 0;
            int saturation = vitalsRecord.HasValue(_configuration.Vitals_Saturation)
                ? GetIntValue(vitalsRecord.GetPropertyText(_configuration.Vitals_Saturation))
                : 0;
            decimal hb = vitalsRecord.HasValue(_configuration.Vitals_HB)
                ? GetDecimalValue(vitalsRecord.GetPropertyText(_configuration.Vitals_HB))
                : 0;

            bool monthly = vitalsRecord.HasValue(_configuration.Vitals_Monthly)
                ? vitalsRecord.GetProperty(+_configuration.Vitals_Monthly).GetValue<bool>()
                : false;

            StoredProc storedProc = new StoredProc();

            storedProc.procedureName = "sp_ExportVitalsRecord";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@Shift", shift);
            storedProc.storedProcParams.Add("@ObjectID", objectId);
            storedProc.storedProcParams.Add("@SiteID", siteId);
            storedProc.storedProcParams.Add("@SiteName", siteName);
            storedProc.storedProcParams.Add("@Resident", resident);
            storedProc.storedProcParams.Add("@DateTaken", dateTaken);
            storedProc.storedProcParams.Add("@Temperature", temperature);
            storedProc.storedProcParams.Add("@SystolicBP", systolicBP);
            storedProc.storedProcParams.Add("@DiastolicBP", diastolicBP);
            storedProc.storedProcParams.Add("@HeartRate", heartRate);
            storedProc.storedProcParams.Add("@Weight", weight);
            storedProc.storedProcParams.Add("@HGT", hgt);
            storedProc.storedProcParams.Add("@Saturation", saturation);
            storedProc.storedProcParams.Add("@HB", hb);
            storedProc.storedProcParams.Add("@Monthly", monthly);

            connector.ExecuteStoredProc(storedProc);
        }

        private decimal GetDecimalValue(string value)
        {
            decimal decimalValue;

            if (!Decimal.TryParse(value, out decimalValue))
                return 0;

            return decimalValue;
        }

        private int GetIntValue(string value)
        {
            int intValue;

            if (!Int32.TryParse(value, out intValue))
                return 0;

            return intValue;
        }
    }
}
