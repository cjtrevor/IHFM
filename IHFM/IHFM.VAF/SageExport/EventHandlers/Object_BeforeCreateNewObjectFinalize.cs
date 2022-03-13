using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.SageExport")]
        public void BeforeNewSageExportCreated(EventHandlerEnvironment env)
        {
            string startTime = env.ObjVerEx.GetProperty(Configuration.SageExport_ExportRangeStart).TypedValue.GetValueAsLocalizedText();
            string endTime = env.ObjVerEx.GetProperty(Configuration.SageExport_ExportRangeEnd).TypedValue.GetValueAsLocalizedText();

            DateTime startDate = DateTime.Parse(startTime);
            DateTime endDate = DateTime.Parse(endTime);

            Lookup siteLookup = env.ObjVerEx.GetProperty(Configuration.SageExport_ExportSite).TypedValue.GetValueAsLookup();
            ObjVerEx site = new ObjVerEx(env.Vault, siteLookup);

            int siteId = site.GetProperty(Configuration.BaseSiteID).TypedValue.GetLookupID();
            string siteName = env.ObjVerEx.GetProperty(Configuration.SageExport_ExportSite).TypedValue.GetValueAsLocalizedText();

            SageExportService service = new SageExportService(env.Vault, Configuration);

            service.ExportBilling(siteId,siteName, startDate, endDate);
        }
    }
}
