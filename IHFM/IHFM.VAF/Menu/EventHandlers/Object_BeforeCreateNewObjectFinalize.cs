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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MenuWeekly")]
        public void OnCreateNewWeeklyMenu(EventHandlerEnvironment env)
        {
            MenuExportService menuExportService = new MenuExportService(Configuration, env.Vault);
            menuExportService.ExportMenu(env.ObjVerEx);
        }
    }
}
