using IHFM.VAF.Resident.BackgroundOperations;
using IHFM.VAF.Utilities;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Configuration.Domain.Dashboards;
using MFiles.VAF.Core;
using MFiles.VAF.Extensions.ScheduledExecution;
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
                TaskQueueBackgroundOperationManager.StartScheduledBackgroundOperation("Monthly Resident Duration Property Updates", Residen_DuratioPropertyUpdates_ProcessingSchedule(), (job) =>
                {
                    new AgeBackgroundOperations().UpdateResidentDurationProperties(job.Vault, Configuration);
                    SysUtils.ReportInfoToEventLog($"IHFM: UpdateResidentDurationProperties completed.");
                });

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

                //Process Resident Site Changes (one item per run to avoid concurrency issues)
                //TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Resident Site Change Propagation",
                //TimeSpan.FromMinutes(1), (job) =>
                //{
                    //Potentially move to DB, add retry logic and find better way to run sequentially without having to wait the full minute between each item when there are multiple items in the queue
                    //But this is a start and works for now since site changes should be relatively infrequent

                    //new ResidentSiteChangeBackgroundOperation().ProcessNextQueueItem(job.Vault, Configuration);
                //});

            }
            catch (Exception e)
            {
                SysUtils.ReportErrorToEventLog("Exception starting background operations", e);
            }
        }


        private Schedule Residen_DuratioPropertyUpdates_ProcessingSchedule()
        {
            var processingTrigger = new Trigger(ScheduleTriggerType.Monthly);
            processingTrigger.DayOfMonthTriggerConfiguration.TriggerDays.Add(1);

            //First time processing each batch gets done every hour, then every 30 minutes, then every 20 minutes
            for (int x = 0; x < 18; x++)
            {
                if (x <= 9)
                {
                    processingTrigger.DayOfMonthTriggerConfiguration.TriggerTimes.Add(new TimeSpan(x, 0, 0));
                }
                else if (x <= 14)
                {
                    processingTrigger.DayOfMonthTriggerConfiguration.TriggerTimes.Add(new TimeSpan(x, 0, 0));
                    processingTrigger.DayOfMonthTriggerConfiguration.TriggerTimes.Add(new TimeSpan(x, 30, 0));
                }
                else
                {
                    for (int i = 0; i < 60; i += 20)
                    {
                        processingTrigger.DayOfMonthTriggerConfiguration.TriggerTimes.Add(new TimeSpan(x, i, 0));
                    }
                }

            }

            var processingSchedule = new Schedule();
            processingSchedule.Triggers.Add(processingTrigger);

            processingSchedule.Enabled = true;

            return processingSchedule;
        }

    }
}