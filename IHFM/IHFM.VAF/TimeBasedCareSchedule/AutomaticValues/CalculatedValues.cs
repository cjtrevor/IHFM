using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
using MFilesAPI;

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
            var frequency = env.ObjVerEx.GetPropertyText(Configuration.TBCS_Frequency);
            var scheduledTimes = env.ObjVerEx.GetPropertyText(Configuration.TBCS_TbcScheduledTimes);
            var assistance = env.ObjVerEx.GetPropertyAsBoolean(Configuration.TBCS_Assistant)??false ? "+A" : "";

            var tbcType = tbcItemObj.GetProperty(Configuration.TBCI_TBCType).TypedValue.GetValueAsLookup();

            var timeAssistance = tbcType?.Item == Configuration.TBCType_CostedADL ? $" ({averageTime}{assistance})" : "";

            var name = $"{tbcItemName}{timeAssistance} {frequency} {scheduledTimes}";

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, name);

            return calculated;
        }
    }
}
