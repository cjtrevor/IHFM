using MFiles.VAF.Common;
using MFilesAPI;
using System.Collections.Generic;

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

        public List<ObjVerEx> GetAllResidentsForSite(string siteNumber)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.ObjType(_configuration.ResidentObject);
            mFSearchBuilder.Property(_configuration.SiteList, MFDataType.MFDatatypeText, siteNumber);
            mFSearchBuilder.Deleted(false);
            return mFSearchBuilder.FindEx();
        }
    }
}
