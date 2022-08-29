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

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_Surname = "MFiles.Property.Surname";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_Initial = "MFiles.Property.Initial";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_AccomodationCalc = "MFiles.Properties.AccomodationCalc";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_DeceasedDeparted = "MFiles.Property.DeceasedOrDeparted";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_NoBowelCount = "MFiles.Property.NobowelCount";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_NoBathCount = "MFiles.Property.NobathCount";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_NoEatCount = "MFiles.Property.NoeatCount";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_DateAdmittedToFacility = "MFiles.Property.DateAdmittedToFacility";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_DateAdmittedToFrailCare = "MFiles.Property.DateAdmittedToFrailCare";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_DateDeceased = "MFiles.Property.DateDeceased";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_IDNumber = "MFiles.Property.IDNumber";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_Site = "MFiles.Property.BaseSite";

        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.Deceaseddeparted")]
        public MFIdentifier DeceasedListItem = "{4930A454-50D9-47BC-9BE2-F08CBCF36D4E}";

        


    }
}
