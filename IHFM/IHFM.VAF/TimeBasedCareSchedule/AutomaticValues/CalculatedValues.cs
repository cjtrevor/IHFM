using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.Tbcscis")]
        public TypedValue SetTimeBasedCareScheduleName(PropertyEnvironment env)
        {
            var tbcItem = env.ObjVerEx.GetProperty(Configuration.TBCS_TimeBasedCareItem).TypedValue.GetValueAsLookup();
            var tbcItemObj = new ObjVerEx(env.Vault, tbcItem);

            var tbcItemName = env.ObjVerEx.GetPropertyText(Configuration.TBCS_TimeBasedCareItem);
            var averageTime = tbcItemObj.GetPropertyText(Configuration.AverageTime);
            var frequency = env.ObjVerEx.GetPropertyText(Configuration.TBCS_Frequency);
            var scheduledTimes = env.ObjVerEx.GetPropertyText(Configuration.TBCS_TbcScheduledTimes);
            var assistance = env.ObjVerEx.GetPropertyAsBoolean(Configuration.TBCS_Assistant)??false ? "+A" : "";

            var name = $"{tbcItemName} ({averageTime}{assistance}) {frequency} {scheduledTimes}";

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, name);

            return calculated;
        }
    }
}
