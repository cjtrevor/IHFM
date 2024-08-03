using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.Shift",Priority = 1)]
        public TypedValue SetShiftValue(PropertyEnvironment env)
        {
            ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration,env.Vault);

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, shiftCalculationService.CalculateShiftNumber(env.ObjVerEx));

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.Dayshift", Priority = 5)]
        public TypedValue SetDayshiftShiftValue(PropertyEnvironment env)
        {
            ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration, env.Vault);

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeBoolean, shiftCalculationService.CalculateShiftNumber(env.ObjVerEx).ToUpper().Contains("DAY"));

            return calculated;
        }
    }
}
