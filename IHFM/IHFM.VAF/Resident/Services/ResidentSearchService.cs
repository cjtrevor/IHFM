using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
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
    }
}
