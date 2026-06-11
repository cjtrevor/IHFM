using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Linq;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.Tbcs")]
        public TypedValue SetTimeBasedCareScheduleName(PropertyEnvironment env)
        {
            var tbcItem = env.ObjVerEx.GetProperty(Configuration.TBCS_TimeBasedCareItem).TypedValue.GetValueAsLookup();
            var tbcItemObj = new ObjVerEx(env.Vault, tbcItem);

            var tbcItemName = tbcItemObj.GetPropertyText(Configuration.TBCS_TimeBasedCareItemItemName);
            var averageTime = tbcItemObj.GetPropertyText(Configuration.AverageTime);
            var frequencyText = "";
            var scheduledTimes = env.ObjVerEx.GetPropertyText(Configuration.TBCS_TbcScheduledTimes);
            var assistance = env.ObjVerEx.GetPropertyAsBoolean(Configuration.TBCS_Assistant) ?? false ? "+A" : "";

            var frequency = env.ObjVerEx.GetProperty(Configuration.TBCS_Frequency).TypedValue.GetValueAsLookup();
            switch (frequency?.ItemGUID)
            {
                case Configuration.ScheduleFrequency_DaysOfWeekGUID:
                    var daysOfWeek = env.ObjVerEx.GetProperty(Configuration.DaysOfWeek).TypedValue.GetValueAsLookups();
                    var selectedDays = daysOfWeek.Cast<Lookup>().Select(item => item.DisplayValue.Substring(0, Math.Min(3, item.DisplayValue.Length))).ToArray();
                    var daysOrder = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
                    frequencyText = string.Join(";", daysOrder.Where(d => selectedDays.Contains(d)));

                    break;
                default:
                    frequencyText = env.ObjVerEx.GetPropertyText(Configuration.TBCS_Frequency);
                    break;
            }

            var tbcType = tbcItemObj.GetProperty(Configuration.TBCI_TBCType).TypedValue.GetValueAsLookup();

            var timeAssistance = tbcType?.Item == Configuration.TBCType_CostedADL ? $" ({averageTime}{assistance})" : "";

            var name = $"{tbcItemName}{timeAssistance} {frequencyText} {scheduledTimes}";

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, name);

            return calculated;
        }
    }
}
