using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef(Required = true)]
        public MFIdentifier SageExport_ExportSite = "MFiles.Property.ExportSite";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SageExport_ExportRangeStart = "MFiles.Property.ExportRangeStart";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SageExport_ExportRangeEnd = "MFiles.Property.ExportRangeEnd";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SageExport_SiteID = "MFiles.Property.BaseSiteId";
    }
}
