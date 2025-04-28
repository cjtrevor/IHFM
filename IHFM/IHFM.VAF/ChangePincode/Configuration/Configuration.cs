using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier ChangePincode_ChangePincodeClass = "MFiles.Class.ChangePincode";

        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier ChangePincode_StaffName = "MFiles.Property.Staffname";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ChangePincode_OldPinCode = "MFiles.Property.OldPinCode";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ChangePincode_NewPinCode = "MFiles.Property.NewPinCode";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ChangePincode_ReEnterNewPinCode = "MFiles.Property.ReenterNewPinCode";
    }
}
