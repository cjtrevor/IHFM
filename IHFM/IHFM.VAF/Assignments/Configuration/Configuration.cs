using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteCarers = "MFiles.Property.SiteCarers";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteIndependantAdministrators = "MFiles.Property.SiteIndependantAdministrators";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteAdministrators = "MFiles.Property.SiteAdministrators";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteVillageManagers = "MFiles.Property.SiteVillageManagers";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteCareManagers = "MFiles.Property.SiteCareManagers";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteTeamLeaders = "MFiles.Property.SiteTeamLeaders";
    }
}
