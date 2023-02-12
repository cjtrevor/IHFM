using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public static class DevelopmentUtility
    {
        public static bool IsDevMode(ObjVerEx obj, Configuration configuration, string siteCode = "999", string siteName = "Renda Consulting" )
        {
            if(obj.HasProperty(configuration.SiteList))
            {
                if(obj.GetProperty(configuration.SiteList).GetValueAsLocalizedText() == siteCode)
                {
                    return true;
                }
            }

            if (obj.HasProperty(configuration.VAFSite))
            {
                if (obj.GetProperty(configuration.VAFSite).GetValueAsLocalizedText() == siteCode)
                {
                    return true;
                }
            }

            if (obj.HasProperty(configuration.BaseSite))
            {
                if (obj.GetProperty(configuration.BaseSite).GetValueAsLocalizedText() == siteName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
