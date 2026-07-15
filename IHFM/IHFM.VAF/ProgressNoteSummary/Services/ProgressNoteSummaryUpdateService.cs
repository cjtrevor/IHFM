using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class ProgressNoteSummaryUpdateService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public ProgressNoteSummaryUpdateService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void LogProgressNoteCreation(ObjVerEx progressNote)
        {
            ObjVerEx currentSummary = GetExistingMonthSummary(progressNote);

            if(currentSummary == null)
            {
                CreateNewProgressNoteSummary(progressNote);
                return;
            }

            int currentAmount = currentSummary.GetProperty(_configuration.ProgressNoteSummary_ProgressNoteCount).GetValue<int>();

            currentSummary.SaveProperty(_configuration.ProgressNoteSummary_ProgressNoteCount, MFDataType.MFDatatypeInteger, ++currentAmount);
        }

        private ObjVerEx GetExistingMonthSummary(ObjVerEx progressNote)
        {
            SiteSearchService siteSearch = new SiteSearchService(_vault, _configuration);
            ObjVerEx site = siteSearch.GetSiteByNumber(progressNote.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());

            string year = DateTime.Today.ToString("yyyy");
            string month = DateTime.Today.ToString("MMMM");

            MFSearchBuilder searchBuilder = new MFSearchBuilder(_vault);
            searchBuilder.Class(_configuration.ProgressNoteSummary_Class);
            searchBuilder.Property(_configuration.ProgressNoteSummary_Site, MFDataType.MFDatatypeLookup, site.ObjID.ID);
            searchBuilder.Property(_configuration.ProgressNoteSummary_Month, MFDataType.MFDatatypeText, month);
            searchBuilder.Property(_configuration.ProgressNoteSummary_Year, MFDataType.MFDatatypeText, year);
            searchBuilder.Property(_configuration.ProgressNoteSummary_NoteType, MFDataType.MFDatatypeLookup, progressNote.GetLookupID(_configuration.ProgressNoteSummary_NoteType));

            if(searchBuilder.FindEx().Count > 0)
            {
                return searchBuilder.FindOneEx();
            }

            return null;


        }

        private void CreateNewProgressNoteSummary(ObjVerEx progressNote)
        {
            SiteSearchService siteSearch = new SiteSearchService(_vault, _configuration);
            ObjVerEx site = siteSearch.GetSiteByNumber(progressNote.GetProperty(_configuration.SiteList).GetValueAsLocalizedText());

            MFPropertyValuesBuilder propertyValuesBuilder = new MFPropertyValuesBuilder(_vault)
            .SetClass(_configuration.ProgressNoteSummary_Class)
            .Add(_configuration.ProgressNoteSummary_BaseSite,MFDataType.MFDatatypeLookup,progressNote.GetLookupID(_configuration.SiteList))
            .Add(_configuration.ProgressNoteSummary_Month, MFDataType.MFDatatypeText, DateTime.Today.ToString("MMMM"))
            .Add(_configuration.ProgressNoteSummary_NoteType, MFDataType.MFDatatypeLookup, progressNote.GetLookupID(_configuration.ProgressNoteSummary_NoteType))
            .Add(_configuration.ProgressNoteSummary_ProgressNoteCount, MFDataType.MFDatatypeInteger, 1)
            .Add(_configuration.ProgressNoteSummary_Site, MFDataType.MFDatatypeLookup, site.ObjID.ID)
            .Add(_configuration.ProgressNoteSummary_Year, MFDataType.MFDatatypeText, DateTime.Today.ToString("yyyy"));

            _vault.ObjectOperations.CreateNewObjectExQuick(_configuration.ProgressNoteSummary_Object.ID, propertyValuesBuilder.Values);
        }
    }
}
