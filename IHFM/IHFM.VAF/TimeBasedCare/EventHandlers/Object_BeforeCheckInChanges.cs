using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCheckInChanges,Class = "MFiles.Class.TBC")]
        public void SetCostForService(EventHandlerEnvironment env)
        {
            TimeBasedCarePropertyService timeBasedCarePropertyService = new TimeBasedCarePropertyService(env.Vault, Configuration);

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
            decimal averageCost = timeBasedCarePropertyService.GetAverageCost(items[1]);

            //Calculate cost
            decimal costForService = timeSpent > averageTime
                                        ? timeSpent * averageCost
                                        : averageTime * averageCost;

            env.ObjVerEx.SetProperty(Configuration.TimeSpent, MFDataType.MFDatatypeText, timeSpent > averageTime ? timeSpent.ToString() : averageTime.ToString());
            env.ObjVerEx.SetProperty(Configuration.CostForService, MFDataType.MFDatatypeText, costForService.ToString("N2"));
            env.ObjVerEx.SaveProperties();
        }
    }
}
