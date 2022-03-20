﻿using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class ScriptControlExportService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;
        private readonly DatabaseConnector _connector;

        public ScriptControlExportService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
            _connector = new DatabaseConnector();
        }

        public void ExportScriptControl(ObjVerEx script)
        {
            SiteSearchService searchService = new SiteSearchService(_vault, _configuration);

            int siteId = Int32.Parse(script.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());
            ObjVerEx site = searchService.GetSiteByNumber(siteId.ToString());
            string siteName = site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();

            DateTime startDate = DateTime.Parse(script.GetProperty(_configuration.ScriptManagementStartDate).GetValueAsLocalizedText());
            DateTime endDate = DateTime.Parse(script.GetProperty(_configuration.ScriptManagementEndDate).GetValueAsLocalizedText());

            int validity = ((startDate.Year - endDate.Year) * 12) + startDate.Month - endDate.Month;

            StoredProc proc = new StoredProc
            {
                procedureName = "sp_ExportScriptControl",
                storedProcParams = new Dictionary<string, object>()
            };

            proc.storedProcParams.Add("@ObjectID", script.ID);
            proc.storedProcParams.Add("@SiteID", siteId);
            proc.storedProcParams.Add("@SiteName", siteName);
            proc.storedProcParams.Add("@ResidentID", script.GetLookupID(_configuration.ResidentLookup));
            proc.storedProcParams.Add("@Resident", script.GetProperty(_configuration.ResidentLookup).GetValueAsLocalizedText());
            proc.storedProcParams.Add("@Validity", validity);
            proc.storedProcParams.Add("@StartDate", startDate);
            proc.storedProcParams.Add("@EndDate", endDate);
            proc.storedProcParams.Add("@Provider", script.GetProperty(_configuration.Provider).GetValueAsLocalizedText());

            _connector.ExecuteStoredProc(proc);
        }

        public void ExportMedsOnScript(ObjVerEx meds, int scriptId)
        {

        }

        private string GetMedsTakenStatus(string medsTaken)
        {
            if (medsTaken.ToUpper() == "REFUSED")
            {
                return "R";
            }
            else if (medsTaken.ToUpper() == "RESIDENT UNAVAILABLE")
            {
                return "U";
            }

            return "T";
        }
        private int GetTimeslot(DateTime timeGiven)
        {
            ScriptManagementUtilityService utilityService = new ScriptManagementUtilityService(_configuration);
            return utilityService.GetScheduledTimeslotNumber(timeGiven);
        }
        public void ExportMedsGiven(ObjVerEx meds, string medsType)
        {
            Lookups medsGiven = meds.GetLookups(_configuration.MedsOnScript);
            SiteSearchService searchService = new SiteSearchService(_vault, _configuration);
            ShiftCalculationService calculationService = new ShiftCalculationService(_configuration, _vault);

            foreach (Lookup med in medsGiven)
            {
                ObjVerEx medObj = new ObjVerEx(_vault, med);

                string medsTakenValue = meds.GetProperty(_configuration.MedsTaken).GetValueAsLocalizedText();
                string medsTaken = GetMedsTakenStatus(medsTakenValue);

                DateTime timeGiven = DateTime.Parse(meds.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText());

                int timeslot = GetTimeslot(timeGiven);

                int siteId = Int32.Parse(meds.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());
                ObjVerEx site = searchService.GetSiteByNumber(siteId.ToString());
                string siteName = site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();

                StoredProc proc = new StoredProc
                {
                    procedureName = "sp_ExportMedsGiven",
                    storedProcParams = new Dictionary<string, object>()
                };

                proc.storedProcParams.Add("@MedsOnScriptID", medObj.ID);
                proc.storedProcParams.Add("@Shift", meds.GetPropertyText(_configuration.Shift));
                proc.storedProcParams.Add("@SiteID", siteId);
                proc.storedProcParams.Add("@Sitename", siteName);
                proc.storedProcParams.Add("@ResidentID", meds.GetLookupID(_configuration.ResidentLookup));
                proc.storedProcParams.Add("@Resident", meds.GetProperty(_configuration.ResidentLookup).GetValueAsLocalizedText());
                proc.storedProcParams.Add("@ObjectID", meds.ID);
                proc.storedProcParams.Add("@MedsTaken", medsTaken);
                proc.storedProcParams.Add("@Timeslot", timeslot);
                proc.storedProcParams.Add("@MedsType", medsType);

                _connector.ExecuteStoredProc(proc);

            }
        }
    }
}