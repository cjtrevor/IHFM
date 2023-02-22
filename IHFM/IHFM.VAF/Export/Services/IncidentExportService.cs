using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class IncidentExportService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public IncidentExportService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void ExportIncident(ObjVerEx incident)
        {
            SiteSearchService searchService = new SiteSearchService(_vault, _configuration);

            int incidentId = incident.ObjVer.ID;
            string incidentName = incident.GetPropertyText(_configuration.ProgressName_ProgressNoteDet);
            string shift = incident.GetPropertyText(_configuration.Shift);
            int siteId = Int32.Parse(incident.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());

            ObjVerEx site = searchService.GetSiteByNumber(siteId.ToString());
            string siteName = site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();

            int residentId = incident.GetLookupID(_configuration.ResidentLookup);
            string resident = incident.GetProperty(_configuration.ResidentLookup).GetValueAsLocalizedText();
            DateTime incidentDate = DateTime.Parse(incident.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText());
            string month = incidentDate.ToString("MMMM", CultureInfo.InvariantCulture);
            int year = incidentDate.Year;


            DatabaseConnector connector = new DatabaseConnector(_configuration.SQLExport_Server, _configuration.SQLExport_Database);

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExpoortIncident";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncidentID", incidentId);
            storedProc.storedProcParams.Add("@Incident", incidentName);
            storedProc.storedProcParams.Add("@Shift", shift);
            storedProc.storedProcParams.Add("@SiteID", siteId);
            storedProc.storedProcParams.Add("@SiteName", siteName);
            storedProc.storedProcParams.Add("@ResidentID", residentId);
            storedProc.storedProcParams.Add("@Resident", resident);
            storedProc.storedProcParams.Add("@IncidentDate", incidentDate);
            storedProc.storedProcParams.Add("@Month", month);
            storedProc.storedProcParams.Add("@Year", year);

            connector.ExecuteStoredProc(storedProc);
        }
    }
}
