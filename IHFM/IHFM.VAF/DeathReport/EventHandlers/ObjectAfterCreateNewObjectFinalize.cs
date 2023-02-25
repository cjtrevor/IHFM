using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.PrintableDeathReportCopy")]
        public void BeforeCreateNewDeathReportRecord(EventHandlerEnvironment env)
        {
            DeathReportExportService exportService = new DeathReportExportService(env.Vault, Configuration);

            exportService.ExportRecord(env.ObjVerEx);
        }
    }
}
