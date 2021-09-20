using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.ActualAmountPaying")]
        public TypedValue SetActualAmountPayableValue(PropertyEnvironment env)
        {
            ResidentAutomaticValueService residentAutomaticValueService = new ResidentAutomaticValueService(Configuration);

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeFloating, residentAutomaticValueService.CalculateActualAmountOutstanding(env.ObjVerEx));

            return calculated;
        }
    }
}
