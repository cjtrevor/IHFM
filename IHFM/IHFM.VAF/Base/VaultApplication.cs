using IHFM.VAF.Utilities;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;
using System;
using System.Collections.Generic;
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



        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChangesFinalize, Class = "MFiles.Class.Triggertest")]
        public void DevTestTriggeRRRRRoonie(EventHandlerEnvironment env)
        {
            return;
            ObjVerEx obj = env.ObjVerEx;

            Lookup resident = obj.GetProperty(Configuration.devTest_Resident).TypedValue.GetValueAsLookup();

            PropertyDef propDef = env.Vault.PropertyDefOperations.GetPropertyDef(3400);

            ValueListItems items = env.Vault.ValueListItemOperations.GetValueListItems(propDef.ValueList);

            var itemDictionary = new Dictionary<int, string>();
            foreach (ValueListItem item in items)
                itemDictionary[item.ID] = item.Name;

            string itemIdAndName = "";

            foreach (ValueListItem item in items)
            {
                int itemId = item.ID;
                string itemName = item.Name;
                itemIdAndName += $"ID: {itemId}, Name: {itemName}\n";
            }

            //throw new Exception($"Resident ID: {resident.DisplayID}\n Value List Items:\n {itemIdAndName}");

            MFSearchBuilder search = new MFSearchBuilder(env.Vault);
            search.Class(Configuration.ResDocs_Class);
            search.Property(Configuration.ResDocs_Resident, MFDataType.MFDatatypeLookup, resident.Item);

            var results = search.FindEx();

            var existingDocTypes = new HashSet<int>();
            var missingDocTypes = new HashSet<int>(itemDictionary.Keys);

            foreach (var item in results)
            {
                var docType = item.GetProperty(Configuration.ResDocs_DocumentType);
                var docTypeId = item.GetLookupID(Configuration.ResDocs_DocumentType);

                if (itemDictionary.ContainsKey(docTypeId))
                    existingDocTypes.Add(docTypeId);
            }

            missingDocTypes.ExceptWith(existingDocTypes);

            var sds = 1;

            //if (search.FindEx().Count > 1)
            //    return search.FindOneEx();

            //return null;

        }

        protected override void StartApplication()
        {
            try
            {
                //Refresh Resident Ages
                //TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Resident Age Refresh",
                //TimeSpan.FromHours(Configuration.AgeRunCheckInterval), (job) =>
                //{
                //    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("RefreshResidentAges", "");

                //    SysUtils.ReportInfoToEventLog(
                //        $"IHFM: ResidentAgeRefresh completed. Next run: {DateTime.Now.AddHours(Configuration.AgeRunCheckInterval)}");
                //});

                //Refresh Average Site Age
                //TaskQueueBackgroundOperationManager.StartRecurringBackgroundOperation("Site Average Age Refresh",
                //TimeSpan.FromHours(Configuration.SiteAverageAgeRunCheckInterval), (job) =>
                //{
                //    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("RefreshSiteAverageAge", "");

                //    SysUtils.ReportInfoToEventLog(
                //        $"IHFM: RefreshSiteAverageAge completed. Next run: {DateTime.Now.AddHours(Configuration.SiteAverageAgeRunCheckInterval)}");
                //});

                TaskQueueBackgroundOperationManager.StartScheduledBackgroundOperation("Daily Resident Progress Note Generation",
                Configuration.GenerateProgressNotesPerResidentSchedule, (job) =>
                {
                    base.PermanentVault.ExtensionMethodOperations.ExecuteVaultExtensionMethod("GenerateProgressNotesPerResident", "");

                    SysUtils.ReportInfoToEventLog(
                        $"IHFM: GenerateProgressNotesPerResident completed");
                });
            }
            catch (Exception e)
            {
                SysUtils.ReportErrorToEventLog("Exception starting background operations", e);
            }
        }
    }
}