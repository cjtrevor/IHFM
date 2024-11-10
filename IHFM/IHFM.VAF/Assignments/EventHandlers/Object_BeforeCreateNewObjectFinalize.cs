using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize)]
        public void SetAssignmentDropdownsOnNew(EventHandlerEnvironment env)
        {
            SiteAssignmentService sas = new SiteAssignmentService(Configuration);
            sas.SetSiteAssignmentProperties(env);
        }

        
    }
}
