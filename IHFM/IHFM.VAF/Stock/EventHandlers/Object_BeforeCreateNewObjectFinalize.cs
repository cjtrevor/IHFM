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
            int stockID = env.ObjVerEx.GetLookupID(Configuration.Stock);
            int siteID = env.ObjVerEx.GetLookupID(Configuration.Site);
            int quantity = env.ObjVerEx.GetProperty(Configuration.StockQuantityIssued).GetValue<int>();
            bool transfer = (bool)env.ObjVerEx.GetProperty(Configuration.Transfer).TypedValue.Value;

            SiteStockUpdateService siteStockUpdateService = new SiteStockUpdateService(env.Vault, Configuration);
            siteStockUpdateService.UpdateSiteStock(siteID, stockID, transfer ? quantity : -quantity) ;
        }
    }
}
