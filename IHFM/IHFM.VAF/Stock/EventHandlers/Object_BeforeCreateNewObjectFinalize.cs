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
            SiteStockUpdateService siteStockUpdateService = new SiteStockUpdateService(env.Vault, Configuration);

            int siteID = env.ObjVerEx.GetLookupID(Configuration.SiteList);
            string transfer = env.ObjVerEx.GetPropertyText(Configuration.Transfer);

            int item1StockID = env.ObjVerEx.GetLookupID(Configuration.Item1Stock);
            if (item1StockID > -1)
            {
                double item1Quantity = env.ObjVerEx.GetProperty(Configuration.Item1StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item1StockID, transfer.ToLower() == "in" ? item1Quantity : -item1Quantity);
            }

            int item2StockID = env.ObjVerEx.GetLookupID(Configuration.Item2Stock);
            if (item2StockID > -1) 
            { 
                double item2Quantity = env.ObjVerEx.GetProperty(Configuration.Item2StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item2StockID, transfer.ToLower() == "in" ? item2Quantity : -item2Quantity);
            }

            int item3StockID = env.ObjVerEx.GetLookupID(Configuration.Item3Stock);           
            if (item3StockID > -1)
            {
                double item3Quantity = env.ObjVerEx.GetProperty(Configuration.Item3StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item3StockID, transfer.ToLower() == "in" ? item3Quantity : -item3Quantity);
            }

            int item4StockID = env.ObjVerEx.GetLookupID(Configuration.Item4Stock);
            if (item4StockID > -1)
            {
                double item4Quantity = env.ObjVerEx.GetProperty(Configuration.Item4StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item4StockID, transfer.ToLower() == "in" ? item4Quantity : -item4Quantity);
            }

            int item5StockID = env.ObjVerEx.GetLookupID(Configuration.Item5Stock);
            if (item5StockID > -1)
            {
                double item5Quantity = env.ObjVerEx.GetProperty(Configuration.Item5StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item5StockID, transfer.ToLower() == "in" ? item5Quantity : -item5Quantity);
            }



        }
    }
}
