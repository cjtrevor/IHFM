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
            DailyCareLogger.Log($"AfterCreateNewDailyCare START — ObjID={env.ObjVerEx.ObjID.ID}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            DailyCareLogger.Log("AfterCreateNewDailyCare — checking NappyChange flag");
            if(env.ObjVerEx.HasValue(Configuration.NappyUsage_NappyChange) && env.ObjVerEx.GetProperty(Configuration.NappyUsage_NappyChange).GetValue<bool>())
            {
                DailyCareLogger.Log("AfterCreateNewDailyCare — NappyChange=true, calling UpdateNappyStock");
                UpdateNappyStock(env.ObjVerEx, env.Vault);
            }

            sw.Stop();
            DailyCareLogger.Log($"AfterCreateNewDailyCare END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.NappyChange")]
        public void AfterCreateNewNappyChange(EventHandlerEnvironment env)
        {
            DailyCareLogger.Log($"AfterCreateNewNappyChange START — ObjID={env.ObjVerEx.ObjID.ID}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            DailyCareLogger.Log("AfterCreateNewNappyChange — checking IncontinenceProduct");
            if(env.ObjVerEx.HasValue(Configuration.IncontinenceSupplies_IncontinenceProduct))
            {
                DailyCareLogger.Log("AfterCreateNewNappyChange — product present, calling UpdateNappyStock");
                UpdateNappyStock(env.ObjVerEx, env.Vault);
            }

            sw.Stop();
            DailyCareLogger.Log($"AfterCreateNewNappyChange END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void UpdateNappyStock(ObjVerEx change, Vault vault)
        {
            DailyCareLogger.Log("UpdateNappyStock START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            NappyUsageService usageService = new NappyUsageService(vault, Configuration);
            IncontinenceStockUpdateService stockUpdateService = new IncontinenceStockUpdateService(vault, Configuration);

            int siteID = change.GetLookupID(Configuration.SiteList);
            int residentId = change.GetLookupID(Configuration.ResidentLookup);

            DailyCareLogger.Log($"UpdateNappyStock — calling LogMonthlyNappyUsage for resident={residentId}");
            usageService.LogMonthlyNappyUsage(siteID, residentId);
            DailyCareLogger.Log("UpdateNappyStock — LogMonthlyNappyUsage done");

            int productId = change.GetLookupID(Configuration.IncontinenceSupplies_IncontinenceProduct);
            DailyCareLogger.Log($"UpdateNappyStock — calling AdjustIncontinenceStockOnHand for resident={residentId}, product={productId}");
            stockUpdateService.AdjustIncontinenceStockOnHand(residentId, productId, -1);
            DailyCareLogger.Log("UpdateNappyStock — AdjustIncontinenceStockOnHand done");

            sw.Stop();
            DailyCareLogger.Log($"UpdateNappyStock END — elapsed={sw.ElapsedMilliseconds}ms");
        }
    }
}
