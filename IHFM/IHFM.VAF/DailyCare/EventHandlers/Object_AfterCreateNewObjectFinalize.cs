using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.DailyCare")]
        public void AfterCreateNewDailyCare(EventHandlerEnvironment env)
        {
            if(env.ObjVerEx.HasValue(Configuration.NappyUsage_NappyChange) && env.ObjVerEx.GetProperty(Configuration.NappyUsage_NappyChange).GetValue<bool>())
                UpdateNappyStock(env.ObjVerEx, env.Vault);
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.NappyChange")]
        public void AfterCreateNewNappyChange(EventHandlerEnvironment env)
        {
            UpdateNappyStock(env.ObjVerEx, env.Vault);
        }

        private void UpdateNappyStock(ObjVerEx change, Vault vault)
        {
            NappyUsageService usageService = new NappyUsageService(vault, Configuration);
            IncontinenceStockUpdateService stockUpdateService = new IncontinenceStockUpdateService(vault, Configuration);

            int siteID = change.GetLookupID(Configuration.SiteList);
            int residentId = change.GetLookupID(Configuration.ResidentLookup);

            usageService.LogMonthlyNappyUsage(siteID, residentId);

            if(DevelopmentUtility.IsDevMode(change,Configuration))
            {
                int productId = change.GetLookupID(Configuration.IncontinenceSupplies_IncontinenceProduct);
                stockUpdateService.AdjustIncontinenceStockOnHand(residentId, productId, -1);
            }
        }
    }
}
