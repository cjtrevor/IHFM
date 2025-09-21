using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef(Required = true)]
        public MFIdentifier Staff_Site = "MFiles.Property.BaseSite";

        public MFIdentifier Staff_SiteId = "MFiles.Property.BaseSiteId";

        public MFIdentifier Staff_GenderTitle = "MFiles.Property.GenderTitle";
    }
}
