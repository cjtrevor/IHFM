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
        public static bool IsDevMode(ObjVerEx obj, Configuration configuration)
        {
            if(obj.HasProperty(configuration.SiteList))
            {
                if(obj.GetProperty(configuration.SiteList).GetValueAsLocalizedText() == "999")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
