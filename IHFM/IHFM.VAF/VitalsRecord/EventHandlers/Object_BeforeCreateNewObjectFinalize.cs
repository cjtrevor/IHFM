using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.VitalsRecord")]
        public void BeforeCreateNewVitalsRecord(EventHandlerEnvironment env)
        {
            VitalsRecordExportService exportService = new VitalsRecordExportService(env.Vault, Configuration);

            exportService.ExportRecord(env.ObjVerEx);
        }
    }
}
