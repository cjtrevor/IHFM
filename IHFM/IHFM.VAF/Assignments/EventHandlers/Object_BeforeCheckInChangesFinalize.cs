using System;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize)]
        public void SetAssignmentDropdownsOnEdit(EventHandlerEnvironment env)
        {
            SiteAssignmentService sas = new SiteAssignmentService(Configuration);
            sas.SetSiteAssignmentProperties(env.ObjVerEx);
        }
    }
}
