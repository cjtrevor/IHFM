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
            {
                DailyCareService dailyCareService = new DailyCareService(env.Vault, Configuration);
                dailyCareService.UpdateNappyStock(env.ObjVerEx);
            }
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.NappyChange")]
        public void AfterCreateNewNappyChange(EventHandlerEnvironment env)
        {
            if(env.ObjVerEx.HasValue(Configuration.IncontinenceSupplies_IncontinenceProduct))
            {
                DailyCareService dailyCareService = new DailyCareService(env.Vault, Configuration);
                dailyCareService.UpdateNappyStock(env.ObjVerEx);
            }
            
        }

        //private void UpdateNappyStock(ObjVerEx change, Vault vault)
        //{
        //    NappyUsageService usageService = new NappyUsageService(vault, Configuration);
        //    IncontinenceStockUpdateService stockUpdateService = new IncontinenceStockUpdateService(vault, Configuration);

        //    int siteID = change.GetLookupID(Configuration.SiteList);
        //    int residentId = change.GetLookupID(Configuration.ResidentLookup);

        //    usageService.LogMonthlyNappyUsage(siteID, residentId);

        //    int productId = change.GetLookupID(Configuration.IncontinenceSupplies_IncontinenceProduct);
        //    stockUpdateService.AdjustIncontinenceStockOnHand(residentId, productId, -1);
        //}
    }
}
