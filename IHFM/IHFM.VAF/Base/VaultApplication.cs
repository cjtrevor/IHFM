using IHFM.VAF.Utilities;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;
using System;
using System.Diagnostics;

namespace IHFM.VAF
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public partial class VaultApplication
        : MFiles.VAF.Extensions.ConfigurableVaultApplicationBase<Configuration>
    {
        protected override void StartApplication()
        {
            try
            {
                //Refresh Resident Ages
                TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Resident Age Refresh",
                TimeSpan.FromHours(Configuration.AgeRunCheckInterval), (job) =>
                {
                    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("RefreshResidentAges", "");

                    SysUtils.ReportInfoToEventLog(
                        $"IHFM: ResidentAgeRefresh completed. Next run: {DateTime.Now.AddHours(Configuration.AgeRunCheckInterval)}");
                });

                //Refresh Average Site Age
                TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Site Average Age Refresh",
                TimeSpan.FromHours(Configuration.SiteAverageAgeRunCheckInterval), (job) =>
                {
                    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("RefreshSiteAverageAge", "");

                    SysUtils.ReportInfoToEventLog(
                        $"IHFM: RefreshSiteAverageAge completed. Next run: {DateTime.Now.AddHours(Configuration.SiteAverageAgeRunCheckInterval)}");
                });

                ////Refresh Site Nominals
                //TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Site Nominals Refresh",
                //TimeSpan.FromHours(Configuration.SiteNominalRunCheckInterval), (job) =>
                //{
                //    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("SetSiteNominals", "");
                //    SysUtils.ReportInfoToEventLog(
                //        $"IHFM: SetSiteNominals completed. Next run: {DateTime.Now.AddHours(Configuration.SiteNominalRunCheckInterval)}");
                //});
            }
            catch (Exception e)
            {
                SysUtils.ReportErrorToEventLog("Exception starting background operations", e);
            }
        }
    }
}