using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.Shift",Priority = 1)]
        public TypedValue SetShiftValue(PropertyEnvironment env)
        {
            SysUtils.ReportInfoToEventLog("Custom Value - Shift");
            ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration,env.Vault);

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, shiftCalculationService.CalculateShiftNumber(env.ObjVerEx));

            return calculated;
        }
    }
}
