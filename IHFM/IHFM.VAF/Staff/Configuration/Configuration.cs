using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier StaffLookup_StaffPinCode = "MFiles.Property.StaffPinCode";

        [MFPropertyDef(Required = true)]
        public MFIdentifier CreatedBy = "MFiles.Property.CreatedBy";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Staff_PinCode = "MFiles.Property.PinCode";
    }
}
