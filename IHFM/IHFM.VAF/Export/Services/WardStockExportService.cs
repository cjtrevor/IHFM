using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public struct WardStockExport
    {
        public int objectId;
        public int siteId;
        public int residentId;
        public Lookup stockId;
        public double quantity;
        public bool isTransferIn;
        public DateTime created;
    }
    public class WardStockExportService
    {
        private Configuration _configuration;
        private Vault _vault;

        public WardStockExportService(Vault vault, Configuration configuration)
        {
            _configuration = configuration;
            _vault = vault;
        }

        public void ExportRecord(WardStockExport item)
        {
            SiteSearchService searchService = new SiteSearchService(_vault, _configuration);
            DatabaseConnector connector = new DatabaseConnector();
            
            ObjVerEx stockItem = new ObjVerEx(_vault, item.stockId);
            ObjVerEx site = searchService.GetSiteByNumber(item.siteId.ToString());
            string siteName = site.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText();

            decimal costPrice = (decimal) stockItem.GetProperty(_configuration.CostPrice).GetValue<double>();
            
            string sellingPriceText = stockItem.GetPropertyText(_configuration.SellingPrice);
            decimal sellingPrice;

            if(!Decimal.TryParse(sellingPriceText, out sellingPrice))
            {
                sellingPrice = 0;
            }

            decimal costParam = 0;
            decimal sellingParam = 0;

            if (item.isTransferIn)
            {
                costParam = costPrice * (decimal)item.quantity;
                sellingParam = 0;
            }
            else
            {
                costParam = 0;
                sellingParam = sellingPrice * (decimal)item.quantity;
            }

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportWardStockRecord";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@ObjectID", item.objectId);
            storedProc.storedProcParams.Add("@SiteID", item.siteId);
            storedProc.storedProcParams.Add("@SiteName", siteName);
            storedProc.storedProcParams.Add("@ResidentID", item.residentId);
            storedProc.storedProcParams.Add("@TranspharmStockID", item.stockId.DisplayID);
            storedProc.storedProcParams.Add("@Direction", item.isTransferIn ? "IN" : "OUT");
            storedProc.storedProcParams.Add("@TransactionDate", item.created);
            storedProc.storedProcParams.Add("@Month", item.created.ToString("MMMM", CultureInfo.InvariantCulture));
            storedProc.storedProcParams.Add("@Year", item.created.Year);
            storedProc.storedProcParams.Add("@CostPrice", costParam);
            storedProc.storedProcParams.Add("@SellingPrice", sellingParam);
            storedProc.storedProcParams.Add("@Quantity", (decimal)item.quantity);

            connector.ExecuteStoredProc(storedProc);
        }
    }
}
