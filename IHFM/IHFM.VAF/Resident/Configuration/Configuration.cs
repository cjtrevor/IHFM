using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Object Aliases
        [MFObjType(Required = true)]
        public MFIdentifier ResidentObject = "MFiles.Object.Resident";

        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier DiscountPercentage = "MFiles.Property.DiscountPercent";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DiscountRandValue = "Mfiles.Property.DiscountRandValue";

        [MFPropertyDef(Required = true)]
        public MFIdentifier RoomTariff = "MFiles.Property.RoomTariff";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TariffVariance = "MFiles.Property.TariffVariance";

        [MFPropertyDef(Required = true)]
        public MFIdentifier CurrentRoom = "MFiles.Property.CurrentRoom";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Active = "MFiles.Property.ResidentActive";

        [MFPropertyDef(Required = true)]
        public MFIdentifier HasCarePlan = "MFiles.Property.HasCarePlan";
    }
}
