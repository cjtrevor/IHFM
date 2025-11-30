using IHFM.VAF.Base;
using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.BatchProcessingConfiguration")]
        public TypedValue SetBatchProcessingValue(PropertyEnvironment env)
        {
            var ModValueSet = env.ObjVerEx.ID % BatchProcessingHelper.BatchCount;

            TypedValue calculated = new TypedValue();

            calculated.SetValue(MFDataType.MFDatatypeInteger, ModValueSet);
            return calculated;
        }
    }
}
