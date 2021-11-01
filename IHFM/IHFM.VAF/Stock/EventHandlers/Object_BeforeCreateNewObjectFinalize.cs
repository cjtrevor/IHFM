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
            SiteStockUpdateService siteStockUpdateService = new SiteStockUpdateService(env.Vault, Configuration);
            WardStockExportService exportService = new WardStockExportService(env.Vault, Configuration);

            int siteID = env.ObjVerEx.GetLookupID(Configuration.VAFSite);
            int siteNumber = Int32.Parse(env.ObjVerEx.GetProperty(Configuration.VAFSite).GetValueAsLocalizedText());

            string transfer = env.ObjVerEx.GetPropertyText(Configuration.Transfer);

            string createdText = env.ObjVerEx.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText();
            DateTime created = DateTime.Parse(createdText);

            int item1StockID = env.ObjVerEx.GetLookupID(Configuration.Item1Stock);
            if (item1StockID > -1)
            {
                Lookup itemLookup = env.ObjVerEx.GetProperty(Configuration.Item1Stock).TypedValue.GetValueAsLookup();
                string itemName = env.ObjVerEx.GetPropertyText(Configuration.Item1Stock);
                double item1Quantity = env.ObjVerEx.GetProperty(Configuration.Item1StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item1StockID, transfer.ToLower() == "in" ? item1Quantity : -item1Quantity, itemName);

                exportService.ExportRecord(new WardStockExport
                {
                    objectId = env.ObjVerEx.ObjID.ID,
                    siteId = siteNumber,
                    isTransferIn = transfer.ToLower() == "in",
                    stockId = itemLookup,
                    created = created,
                    quantity = siteStockUpdateService.GetConvertedQuantity(item1StockID,item1Quantity)
                });
            }

            int item2StockID = env.ObjVerEx.GetLookupID(Configuration.Item2Stock);
            if (item2StockID > -1) 
            {
                Lookup itemLookup = env.ObjVerEx.GetProperty(Configuration.Item2Stock).TypedValue.GetValueAsLookup();
                string itemName = env.ObjVerEx.GetPropertyText(Configuration.Item2Stock);
                double item2Quantity = env.ObjVerEx.GetProperty(Configuration.Item2StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item2StockID, transfer.ToLower() == "in" ? item2Quantity : -item2Quantity, itemName);
                exportService.ExportRecord(new WardStockExport
                {
                    objectId = env.ObjVerEx.ObjID.ID,
                    siteId = siteNumber,
                    isTransferIn = transfer.ToLower() == "in",
                    stockId = itemLookup,
                    created = created,
                    quantity = siteStockUpdateService.GetConvertedQuantity(item2StockID, item2Quantity)
                });
            }

            int item3StockID = env.ObjVerEx.GetLookupID(Configuration.Item3Stock);           
            if (item3StockID > -1)
            {
                Lookup itemLookup = env.ObjVerEx.GetProperty(Configuration.Item3Stock).TypedValue.GetValueAsLookup();
                string itemName = env.ObjVerEx.GetPropertyText(Configuration.Item3Stock);
                double item3Quantity = env.ObjVerEx.GetProperty(Configuration.Item3StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item3StockID, transfer.ToLower() == "in" ? item3Quantity : -item3Quantity, itemName);
                exportService.ExportRecord(new WardStockExport
                {
                    objectId = env.ObjVerEx.ObjID.ID,
                    siteId = siteNumber,
                    isTransferIn = transfer.ToLower() == "in",
                    stockId = itemLookup,
                    created = created,
                    quantity = siteStockUpdateService.GetConvertedQuantity(item3StockID, item3Quantity)
                });
            }

            int item4StockID = env.ObjVerEx.GetLookupID(Configuration.Item4Stock);
            if (item4StockID > -1)
            {
                Lookup itemLookup = env.ObjVerEx.GetProperty(Configuration.Item4Stock).TypedValue.GetValueAsLookup();
                string itemName = env.ObjVerEx.GetPropertyText(Configuration.Item4Stock);
                double item4Quantity = env.ObjVerEx.GetProperty(Configuration.Item4StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item4StockID, transfer.ToLower() == "in" ? item4Quantity : -item4Quantity, itemName);
                exportService.ExportRecord(new WardStockExport
                {
                    objectId = env.ObjVerEx.ObjID.ID,
                    siteId = siteNumber,
                    isTransferIn = transfer.ToLower() == "in",
                    stockId = itemLookup,
                    created = created,
                    quantity = siteStockUpdateService.GetConvertedQuantity(item4StockID, item4Quantity)
                });
            }

            int item5StockID = env.ObjVerEx.GetLookupID(Configuration.Item5Stock);
            if (item5StockID > -1)
            {
                Lookup itemLookup = env.ObjVerEx.GetProperty(Configuration.Item5Stock).TypedValue.GetValueAsLookup();
                string itemName = env.ObjVerEx.GetPropertyText(Configuration.Item5Stock);
                double item5Quantity = env.ObjVerEx.GetProperty(Configuration.Item5StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item5StockID, transfer.ToLower() == "in" ? item5Quantity : -item5Quantity, itemName);
                exportService.ExportRecord(new WardStockExport
                {
                    objectId = env.ObjVerEx.ObjID.ID,
                    siteId = siteNumber,
                    isTransferIn = transfer.ToLower() == "in",
                    stockId = itemLookup,
                    created = created,
                    quantity = siteStockUpdateService.GetConvertedQuantity(item5StockID, item5Quantity)
                });
            }

            if (transfer.ToLower() == "in")
            {
                env.ObjVerEx.RemoveProperty(Configuration.ResidentLookup);
                env.ObjVerEx.SaveProperties();
            }
        }
    }
}
