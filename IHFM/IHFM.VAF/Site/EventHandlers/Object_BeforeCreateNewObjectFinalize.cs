﻿using MFiles.VAF.Common;
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
        //[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.StockIssue")]
        public void SetStockIssueSite(EventHandlerEnvironment env)
        {
            SitePermissionService sitePermissionService = new SitePermissionService(env.Vault, Configuration);
            sitePermissionService.SetSiteFromStaffByUserID(env.ObjVerEx);
        }
    }
}
