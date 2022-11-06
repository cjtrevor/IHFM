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
        [PropertyCustomValue("MFiles.Property.StockSite")]
        public TypedValue SetSiteStockSiteName(PropertyEnvironment env)
        {
            string siteName = "Not Found";
            TypedValue calculated = new TypedValue();
            SiteSearchService searchService = new SiteSearchService(env.Vault, Configuration);

            int siteNumber = -1;
            Int32.TryParse(env.ObjVerEx.GetProperty(Configuration.VAFSite).GetValueAsLocalizedText(), out siteNumber);

            if (siteNumber > 0)
            { 
                ObjVerEx site = searchService.GetSiteByNumber(siteNumber.ToString());
                siteName = site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();
            }
            calculated.SetValue(MFDataType.MFDatatypeText, siteName);

            return calculated;
        }
    }
}
