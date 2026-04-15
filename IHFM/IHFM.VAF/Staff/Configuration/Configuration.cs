using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Property Aliases
        [MFPropertyDef]
        public MFIdentifier StaffLookup_StaffPinCode = "MFiles.Property.StaffPinCode";
        [MFPropertyDef]
        public MFIdentifier StaffLookup_StaffPassword = "MFiles.Property.StaffPassword";

        [MFPropertyDef]
        public MFIdentifier CreatedBy = "MFiles.Property.CreatedBy";

        [MFPropertyDef]
        public MFIdentifier Staff_PinCode = "MFiles.Property.PinCode";
        [MFPropertyDef]
        public MFIdentifier Staff_Password = "MFiles.Property.Password";
    }
}
