using MFilesAPI;
using System;
using MFiles.VAF.Common;

namespace IHFM.VAF
{
    
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.StockIssue")]
        public void CreateNewStockIssue(EventHandlerEnvironment env)

        {
            SetStockIssueSite(env);
            int siteID = env.ObjVerEx.GetLookupID(Configuration.VAFSite);

            SiteStockUpdateService service = new SiteStockUpdateService(env.Vault, Configuration);
            service.CreateNewStockIssue(siteID, env.ObjVerEx);
        }
    }
}
