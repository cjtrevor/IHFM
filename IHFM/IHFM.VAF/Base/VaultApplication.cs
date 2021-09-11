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
                TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Resident Age Refresh",
                TimeSpan.FromHours(Configuration.AgeRunCheckInterval), (job) =>
                {
                    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("RefreshResidentAges", "");
                                                
                    SysUtils.ReportInfoToEventLog(
                        $"IHFM: ResidentAgeRefresh completed. Next run: {DateTime.Now.AddHours(Configuration.AgeRunCheckInterval)}");
                });
            }
            catch (Exception e)
            {
                SysUtils.ReportErrorToEventLog("Exception starting background operations", e);
            }
        }
    }
}