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
        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier Staff = "MFiles.Class.Staff";

        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site = "MFiles.Property.Site";
        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteList = "MFiles.Properties.SiteList";
        [MFPropertyDef(Required = true)]
        public MFIdentifier BaseSiteID = "MFiles.Property.BaseSiteId";
        [MFPropertyDef(Required = true)]
        public MFIdentifier BaseSite = "MFiles.Property.BaseSite";
        [MFPropertyDef(Required = true)]
        public MFIdentifier VAFSite = "MFiles.Properties.VAFSite";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Login = "MFiles.Property.Login";
    }
}
