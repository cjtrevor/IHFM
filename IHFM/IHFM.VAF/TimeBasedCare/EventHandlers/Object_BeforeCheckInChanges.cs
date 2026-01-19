using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;
using MFiles.VAF.Extensions;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges,Class = "MFiles.Class.TBC")]
        public void SetCostForService(EventHandlerEnvironment env)
        {
            try
            {
                if(!env.ObjVerEx.HasValue(Configuration.EndTime))
                {
                    return;
                }

                if(!env.ObjVerEx.HasValue(Configuration.TBCADLLookup))
                {
                    throw new Exception("You cannot save the record without having any ADL(TBC) items selected.");
                }

                TimeBasedCarePropertyService timeBasedCarePropertyService = new TimeBasedCarePropertyService(env.Vault, Configuration);
                TBCExportService exportService = new TBCExportService(env.Vault, Configuration);

                //Calculate time spent
                string startTime = env.ObjVerEx.GetProperty(Configuration.StartTimeTBC).TypedValue.GetValueAsLocalizedText();
                string endTime = env.ObjVerEx.GetProperty(Configuration.EndTime).TypedValue.GetValueAsLocalizedText();

                DateTime startDate = DateTime.Parse($"2000-01-01 {startTime}");
                DateTime endDate = DateTime.Parse($"2000-01-01 {endTime}");

                int timeSpent = (int)(endDate - startDate).TotalMinutes;

                int averageTime = 0;
                //Calculate average time
                Lookups items = env.ObjVerEx.GetLookups(Configuration.TBCADLLookup);
                foreach (Lookup item in items)
                {
                    averageTime += timeBasedCarePropertyService.GetAverageTime(item);
                }

                //Get cost from first item
                decimal averageCost = items.Count > 0 ? timeBasedCarePropertyService.GetAverageCost(items[1]) : 0;

                //Calculate cost
                decimal costForService = timeSpent > averageTime
                                            ? timeSpent * averageCost
                                            : averageTime * averageCost;

                env.ObjVerEx.SetProperty(Configuration.TimeSpent, MFDataType.MFDatatypeText, timeSpent > averageTime ? timeSpent.ToString() : averageTime.ToString());
                env.ObjVerEx.SetProperty(Configuration.CostForService, MFDataType.MFDatatypeText, costForService.ToString("N2"));
                env.ObjVerEx.SaveProperties();

                exportService.ExportRecord(env.ObjVerEx, TBCExportService.TbcType.TBC);
            }
            catch (Exception ex)
            {

                throw new Exception("TBC - Set Cost For Service - " + ex.Message);
            }
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, ObjectType = "MFiles.Object.TBCClinic")]
        public void SetClinicCostForService(EventHandlerEnvironment env)
        {
            if (!env.ObjVerEx.HasValue(Configuration.EndTime))
            {
                return;
            }

            if (!env.ObjVerEx.HasValue(Configuration.TBCClinicLookup))
            {
                throw new Exception("You cannot save the record without having any ADL(Clinic) items selected.");
            }

            TimeBasedCarePropertyService timeBasedCarePropertyService = new TimeBasedCarePropertyService(env.Vault, Configuration);
            TBCExportService exportService = new TBCExportService(env.Vault, Configuration);

            //Calculate time spent
            string startTime = env.ObjVerEx.GetProperty(Configuration.StartTimeTBC).TypedValue.GetValueAsLocalizedText();
            string endTime = env.ObjVerEx.GetProperty(Configuration.EndTime).TypedValue.GetValueAsLocalizedText();

            DateTime startDate = DateTime.Parse($"2000-01-01 {startTime}");
            DateTime endDate = DateTime.Parse($"2000-01-01 {endTime}");

            int timeSpent = (int)(endDate - startDate).TotalMinutes;

            int averageTime = 0;
            //Calculate average time
            Lookups items = env.ObjVerEx.GetLookups(Configuration.TBCClinicLookup);
            foreach (Lookup item in items)
            {
                averageTime += timeBasedCarePropertyService.GetAverageTime(item);
            }

            //Get cost from first item
            decimal averageCost = items.Count > 0 ? timeBasedCarePropertyService.GetAverageCost(items[1]) : 0;

            //Calculate cost
            decimal costForService = timeSpent > averageTime
                                        ? timeSpent * averageCost
                                        : averageTime * averageCost;

            env.ObjVerEx.SetProperty(Configuration.TimeSpent, MFDataType.MFDatatypeText, timeSpent > averageTime ? timeSpent.ToString() : averageTime.ToString());
            env.ObjVerEx.SetProperty(Configuration.CostForService, MFDataType.MFDatatypeText, costForService.ToString("N2"));
            env.ObjVerEx.SaveProperties();

            exportService.ExportRecord(env.ObjVerEx, TBCExportService.TbcType.Clinic);
        }

        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.CareItemsSchedule")]
        public void TimeBasedCareScheduleValidation(EventHandlerEnvironment env)
        {
            var hasFrequency = env.ObjVerEx.HasProperty(Configuration.TBCS_Frequency) && env.ObjVerEx.HasValue(Configuration.TBCS_Frequency);

            if (hasFrequency && env.ObjVerEx.GetLookupID(Configuration.TBCS_Frequency) == Configuration.Frequency_OnceOff.ID)
            {
                var hasOnceOffDate = env.ObjVerEx.HasProperty(Configuration.TBCS_OnceOffDate) && env.ObjVerEx.HasValue(Configuration.TBCS_OnceOffDate);
                if (!hasOnceOffDate)
                    throw new Exception("Once Off Date is required when Frequency is set to Once Off.");

                var hasTimeSlot = env.ObjVerEx.HasProperty(Configuration.TBCS_TbcScheduledTimes) && env.ObjVerEx.HasValue(Configuration.TBCS_TbcScheduledTimes);
                if (!hasTimeSlot)
                    throw new Exception("At least one Time Slot is required when Frequency is set to Once Off.");
            }
        }
    }
}
