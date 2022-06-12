

using IHFM.VAF.Export.Classes;
using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;

namespace IHFM.VAF.Export.Services
{
    public class QMRAdmissionExportService
    {
        private readonly Configuration _configuration;
        private readonly Vault _vault;
        private readonly SiteSearchService _siteSearch;

        public QMRAdmissionExportService(Configuration configuration, Vault vault)
        {
            _configuration = configuration;
            _vault = vault;
            _siteSearch = new SiteSearchService(_vault, _configuration);
        }

        public void Export(ObjVerEx admission)
        {
            QMRAdmissionsExport export = new QMRAdmissionsExport();
            export.ObjId = admission.ObjID.ID;
            export.SiteID = Int32.Parse(admission.GetProperty(_configuration.DailyCare_Site).GetValueAsLocalizedText());
            export.SiteName = GetSiteName(admission);

            UpdateResidentDetails(export, admission);
            UpdateDateInfo(export, admission);
        }

        private void UpdateResidentDetails(QMRAdmissionsExport export, ObjVerEx admission)
        {
            Lookup residentLookup = admission.GetProperty(_configuration.DailyCare_Resident).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(_vault, residentLookup);

            int age = 0;
            Int32.TryParse(resident.GetPropertyText(_configuration.Resident_Age), out age);

            export.ResidentName = resident.GetPropertyText(_configuration.Resident_ResidentDetail);
            export.Age = age;
            export.Sex = resident.GetPropertyText(_configuration.Resident_GenderTitle);
            export.MedicalConditions = GetMedicalConditions(resident.GetProperty(_configuration.Resident_MedicalConditions).TypedValue.GetValueAsLookups());
        }

        private string GetMedicalConditions(Lookups conditions)
        {
            List<string> cons = new List<string>();
            foreach(Lookup con in conditions)
            {
                cons.Add(con.DisplayValue);
            }

            return String.Join(",",cons);
        }

        private void UpdateDateInfo(QMRAdmissionsExport export, ObjVerEx admission)
        {
            string created = admission.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText();
            DateTime dtCreated = DateTime.Parse(created);

            export.YearNumber = dtCreated.Year;
            export.QuarterNumber = dtCreated.QuarterDecStart();
            export.DateAdmitted = created;
        }

        private string GetSiteName(ObjVerEx admission)
        {
            int siteId = Int32.Parse(admission.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());
            ObjVerEx site = _siteSearch.GetSiteByNumber(siteId.ToString());
            return site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();
        }
    }
}
