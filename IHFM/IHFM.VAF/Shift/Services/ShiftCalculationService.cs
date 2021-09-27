using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public class ShiftCalculationService
    {
        private Configuration _configuration;
        private Vault _vault;
        public ShiftCalculationService(Configuration configuration, Vault vault)
        {
            _configuration = configuration;
            _vault = vault;
        }

        private string GetShiftStartTimeFromSite(ObjVerEx objVerEx)
        {
            SiteSearchService siteSearchService = new SiteSearchService(_vault, _configuration);
            SitePermissionService sitePermissionService = new SitePermissionService(_vault, _configuration);

            //string siteNum;

            //if (objVerEx.HasProperty(_configuration.BaseSite))
            //{
            //    siteNum = objVerEx.GetProperty(_configuration.BaseSite).TypedValue.GetValueAsLocalizedText();
            //}
            //else if (objVerEx.HasProperty(_configuration.SiteList))
            //{
            //    siteNum = objVerEx.GetProperty(_configuration.SiteList).TypedValue.GetValueAsLocalizedText();
            //}
            //else
            //    return "";

            int createdByID = objVerEx.Properties.SearchForProperty((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCreatedBy).TypedValue.GetLookupID();
            string siteNum = sitePermissionService.GetSiteNumByUserID(createdByID);

            ObjVerEx siteObj = siteSearchService.GetSiteByNumber(siteNum);
            return siteObj.GetProperty(_configuration.ShiftStartTime).TypedValue.GetValueAsLocalizedText();
        }
        public string CalculateShiftNumber(ObjVerEx objVerEx)
        {
            string dateNow = objVerEx.GetPropertyText(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated);

            string shiftStartString = GetShiftStartTimeFromSite(objVerEx);

            DateTime shiftStartTime = DateTime.Parse($"01/01/2000 {shiftStartString}"); //Lookup from site object
            DateTime created = DateTime.Parse(dateNow);

            int shiftStartHour = shiftStartTime.Hour;
            int createdHour = created.Hour; //Created is in UTC time

            string yearPart = created.Year.ToString().Substring(2, 2);
            string monthPart;
            string dayPart;
            string ShiftIndicator = "";

            if (createdHour < shiftStartHour)
            {
                monthPart = (created.AddDays(-1).Month).ToString().PadLeft(2, '0');
            }
            else
            {
                monthPart = (created.Month).ToString().PadLeft(2, '0');
            }

            if (createdHour < shiftStartHour)
            {
                dayPart = (created.AddDays(-1).Day).ToString().PadLeft(2, '0');
            }
            else
            {
                dayPart = (created.Day).ToString().PadLeft(2, '0');
            }

            if(createdHour >= shiftStartHour && createdHour < shiftStartHour + 12)
            {
                ShiftIndicator = "Day";
            }
            else if(createdHour >= shiftStartHour + 12 || createdHour < shiftStartHour)
            {
                ShiftIndicator = "Night";
            }

            return $"{dayPart}{monthPart}{yearPart}-{ShiftIndicator}";
        }
    }
}
