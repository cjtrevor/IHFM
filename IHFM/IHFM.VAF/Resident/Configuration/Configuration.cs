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

        [MFPropertyDef(Required = true)]
        public MFIdentifier OnCarePlan = "MFiles.Property.OnCarePackage";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_CPOARef = "MFiles.Property.CPOARef";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_ActualAmountPayable = "MFiles.Property.ActualAmountPaying";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_ResidentDetail = "MFiles.Property.ResidentDetail";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_Age = "MFiles.Property.Age";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_GenderTitle = "MFiles.Property.GenderTitle";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_MedicalConditions = "MFiles.Property.MedicalConditions";
    }
}
