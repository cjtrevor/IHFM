using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class SiteSearchService
    {
        private Vault _vault;
        private Configuration _configuration;
        public SiteSearchService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public ObjVerEx GetSiteByNumber(string number)
        {
            MFSearchBuilder mFSearch = new MFSearchBuilder(_vault);
            mFSearch.ObjType(_configuration.SiteObject);
            mFSearch.Deleted(false);
            mFSearch.Property(_configuration.BaseSiteID, MFDataType.MFDatatypeText, number);

            return mFSearch.FindOneEx();
        }

        public List<ObjVerEx> GetAllSites()
        {
            MFSearchBuilder mFSearch = new MFSearchBuilder(_vault);
            mFSearch.ObjType(_configuration.SiteObject);
            mFSearch.Deleted(false);

            return mFSearch.FindEx();
        }
    }
}
