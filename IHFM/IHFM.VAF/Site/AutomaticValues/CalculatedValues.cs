using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.ReportSite", Priority = 1)]
        public TypedValue SetReportSite(PropertyEnvironment env)
        {
            string siteName = "";

            if(env.ObjVerEx.HasProperty(Configuration.SiteList) && env.ObjVerEx.HasValue(Configuration.SiteList))
            {
                SiteSearchService siteSearch = new SiteSearchService(env.Vault, Configuration);
                ObjVerEx site = siteSearch.GetSiteByNumber(env.ObjVerEx.GetProperty(Configuration.SiteList).GetValueAsLocalizedText());
                
                if(site != null)
                {
                    siteName = site.GetPropertyText(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle);
                }
            }

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, siteName);

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.SiteidBySite", Priority = 1)]
        public TypedValue SetSiteIdBySite(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            if (!env.ObjVerEx.HasProperty(Configuration.BaseSite) || !env.ObjVerEx.HasValue(Configuration.BaseSite))
            {
                return calculated;
            }

            Lookup lookupSite = env.ObjVerEx.GetProperty(Configuration.BaseSite).TypedValue.GetValueAsLookup();
            ObjVerEx site = new ObjVerEx(env.Vault, lookupSite);

            calculated.SetValue(MFDataType.MFDatatypeLookup, site.GetLookupID(Configuration.BaseSiteID));
            return calculated;
        }
    }
}
