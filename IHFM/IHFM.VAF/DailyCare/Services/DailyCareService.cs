using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public class DailyCareService
    {
        private readonly Vault vault;
        private readonly Configuration configuration;

        public DailyCareService(Vault vault, Configuration configuration)
        {
            this.vault = vault;
            this.configuration = configuration;
        }

        public void UpdateNappyStock(ObjVerEx objVerEx)
        {
            NappyUsageService usageService = new NappyUsageService(vault, configuration);
            IncontinenceStockUpdateService stockUpdateService = new IncontinenceStockUpdateService(vault, configuration);

            int siteID = objVerEx.GetLookupID(configuration.SiteList);
            int residentId = objVerEx.GetLookupID(configuration.ResidentLookup);

            usageService.LogMonthlyNappyUsage(siteID, residentId);

            int productId = objVerEx.GetLookupID(configuration.IncontinenceSupplies_IncontinenceProduct);
            stockUpdateService.AdjustIncontinenceStockOnHand(residentId, productId, -1);
        }

    }
}
