using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace IHFM.VAF
{
    public class ResidentSearchService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public ResidentSearchService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }
        public List<ObjVerEx> GetAllResidents()
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.ObjType(_configuration.ResidentObject);
            mFSearchBuilder.Deleted(false);
            return mFSearchBuilder.FindEx();
        }

        public List<ObjVerEx> GetAllResidentsWithDobToday()
        {
            DateTime today = DateTime.Today;
            int currentDay = today.Day;
            int currentMonth = today.Month;

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.ObjType(_configuration.ResidentObject);
            mFSearchBuilder.Property(_configuration.DobDay, MFDataType.MFDatatypeText, currentDay);
            mFSearchBuilder.Property(_configuration.DobMonth, MFDataType.MFDatatypeText, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth));
            mFSearchBuilder.PropertyEmpty(_configuration.Resident_DateDeceased);
            mFSearchBuilder.Deleted(false);
            return mFSearchBuilder.FindEx();
        }

        public List<ObjVerEx> GetAllResidentsBeforeDurationLastUpdatedDateByBatch(DateTime durationLastUpdatedDate, int batchValue)
        {
            var residentList = new List<ObjVerEx>();

            MFSearchBuilder mFSearchBuilderEmpty = new MFSearchBuilder(_vault);
            mFSearchBuilderEmpty.ObjType(_configuration.ResidentObject);
            mFSearchBuilderEmpty.Property(_configuration.Base_BatchProcessingConfiguration, batchValue);
            mFSearchBuilderEmpty.PropertyEmpty(_configuration.Resident_DurationsLastUpdated);
            mFSearchBuilderEmpty.PropertyEmpty(_configuration.Resident_DateDeceased);
            mFSearchBuilderEmpty.Property(_configuration.Resident_DeceasedDeparted, _configuration.DeceasedListItem.ID, MFConditionType.MFConditionTypeNotEqual);
            mFSearchBuilderEmpty.Property(_configuration.Resident_DeceasedDeparted, _configuration.DischargedListItem.ID, MFConditionType.MFConditionTypeNotEqual);
            mFSearchBuilderEmpty.Deleted(false);

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.ObjType(_configuration.ResidentObject);
            mFSearchBuilder.Property(_configuration.Base_BatchProcessingConfiguration, batchValue);
            mFSearchBuilder.PropertyEmpty(_configuration.Resident_DateDeceased);
            mFSearchBuilder.Property(_configuration.Resident_DeceasedDeparted, _configuration.DeceasedListItem.ID, MFConditionType.MFConditionTypeNotEqual);
            mFSearchBuilder.Property(_configuration.Resident_DeceasedDeparted, _configuration.DischargedListItem.ID, MFConditionType.MFConditionTypeNotEqual);
            mFSearchBuilder.Date(_configuration.Resident_DurationsLastUpdated, durationLastUpdatedDate, MFConditionType.MFConditionTypeLessThanOrEqual);
            mFSearchBuilder.Deleted(false);

            residentList.AddRange(mFSearchBuilderEmpty.FindEx());
            residentList.AddRange(mFSearchBuilder.FindEx());

            return residentList;
        }

        public List<ObjVerEx> GetAllActiveResidents()
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.ObjType(_configuration.ResidentObject);
            mFSearchBuilder.Property(_configuration.Active, MFDataType.MFDatatypeBoolean, true);
            mFSearchBuilder.Deleted(false);
            return mFSearchBuilder.FindEx();
        }

        public List<ObjVerEx> GetAllResidentsForSite(int siteNumber)
        {
            List<ObjVerEx> allResident = GetAllActiveResidents();

            return allResident.Where(x => x.GetLookupID(_configuration.BaseSiteID) == siteNumber).ToList();
        }

        public List<ObjVerEx> GetResidentsBySiteAndZone(int siteNumber, List<int> zoneIds)
        {
            List<ObjVerEx> siteResidents = GetAllResidentsForSite(siteNumber);

            return siteResidents.Where(x => zoneIds.Contains(x.GetLookupID(_configuration.Room_Zone))).ToList();
        }

        public ObjVerEx GetResidentByIDNumber(string idNumber)
        {
            MFSearchBuilder resSearch = new MFSearchBuilder(_vault);
            resSearch.ObjType(_configuration.ResidentObject);
            resSearch.Property(_configuration.IDNumber, MFDataType.MFDatatypeText, idNumber);
            resSearch.Deleted(false);
            
            List<ObjVerEx> results = resSearch.FindEx();

            if (results.Count > 0)
                return results.FirstOrDefault();

            return null;
        }

        public List<ObjVerEx> GetResidentsByIDNumber(string idNumber)
        {
            MFSearchBuilder resSearch = new MFSearchBuilder(_vault);
            resSearch.ObjType(_configuration.ResidentObject);
            resSearch.Property(_configuration.IDNumber, MFDataType.MFDatatypeText, idNumber);
            resSearch.Deleted(false);

            return resSearch.FindEx();
        }

        public List<ObjVerEx> GetResidentByRoom(int roomId)
        {
            MFSearchBuilder resSearch = new MFSearchBuilder(_vault);
            resSearch.ObjType(_configuration.ResidentObject);
            resSearch.Property(_configuration.CurrentRoom, MFDataType.MFDatatypeLookup, roomId);
            resSearch.Deleted(false);

            return resSearch.FindEx();
        }
    }
}
