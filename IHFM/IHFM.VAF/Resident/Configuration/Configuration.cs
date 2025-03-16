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
        public MFIdentifier Resident_ResidentOrMember = "MFiles.Property.ResidentOrMember";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_Age = "MFiles.Property.Age";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_GenderTitle = "MFiles.Property.GenderTitle";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_MedicalConditions = "MFiles.Property.MedicalConditions";

        [MFPropertyDef]
        public MFIdentifier Resident_FirstName = "PD.FirstName";

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

        [MFPropertyDef]
        public MFIdentifier Resident_AccomodationRequired = "MFiles.Property.AccomodationRequired";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_DateDeceased = "MFiles.Property.DateDeceased";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_IDNumber = "MFiles.Property.IDNumber";

        [MFPropertyDef]
        public MFIdentifier Resident_HealthStatus = "MFiles.Property.Independant";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Resident_Site = "MFiles.Property.BaseSite";

        [MFPropertyDef]
        public MFIdentifier Resident_DurationInFacility = "MFiles.Property.DurationOfStayInFacility";

        [MFPropertyDef]
        public MFIdentifier Resident_DurationInFrailcare = "MFiles.Property.DurationOfStayInFrailcare";

        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.Deceaseddeparted")]
        public MFIdentifier DeceasedListItem = "{4930A454-50D9-47BC-9BE2-F08CBCF36D4E}";

        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.Deceaseddeparted")]
        public MFIdentifier DischargedListItem = "{7C24698D-E37E-49E8-A0D7-2C80F218AEFF}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.Deceaseddeparted")]
        public MFIdentifier HospitalListItem = "{ADFA2DF4-8EA7-43A6-A432-B539F77FAA5E}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.Deceaseddeparted")]
        public MFIdentifier TempDischargeListItem = "{C277F52F-0843-4D93-B4C8-471E558C4B1D}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.Deceaseddeparted")]
        public MFIdentifier ReturnedToResidenceListItem = "{844CEC6A-7BD8-4605-87AB-BAD3A1ED95EF}";

        //250314
        [MFPropertyDef]
        public MFIdentifier Resident_EnquiryType = "MFiles.Property.EnquiryType";
        [MFValueList(Required = true)]
        public MFIdentifier Resident_EnquiryTypeValueList = "MFiles.ValueList.Enquirytype";

        [MFValueList(Required = true)]
        public MFIdentifier Resident_EnquiryTypeValueListItem_CareCentre = "{CA894CB2-4215-4D9A-8E3F-0BD2EF8ABCC8}";
        [MFValueList(Required = true)]
        public MFIdentifier Resident_EnquiryTypeValueListItem_IndependentLiving = "{22E3E268-18EF-41D3-833F-71ED7F486F09}";
        

        [MFPropertyDef]
        public MFIdentifier Resident_LeadSource = "MFiles.Property.LeadSource";
        [MFValueList(Required = true)]
        public MFIdentifier Resident_LeadSourceValueList = "MFiles.ValueList.Leadsource";

        [MFPropertyDef]
        public MFIdentifier Resident_AccommodationUrgency = "MFiles.Property.AccommodationUrgency";//TODO: ADD ALIAS IN MFILES
        [MFValueList(Required = true)]
        public MFIdentifier Resident_AccommodationUrgencyValueList = "MFiles.Valuelist.AccommodationUrgency";

        [MFPropertyDef]
        public MFIdentifier Resident_ApplicationDate = "MFiles.Property.ApplicationDate"; //TODO: ADD ALIAS IN MFILES

        [MFValueList(Required = true)]
        public MFIdentifier Resident_AccomodationRequiredValueList = "MFiles.ValueList.RoomTypes"; //TODO: ADD ALIAS IN MFILES

        [MFPropertyDef]
        public MFIdentifier Resident_CellPhoneNumber = "PD.OwnCellPhone";

        [MFClass(Required = true)]
        public MFIdentifier ResidentClass = "MFiles.Class.Resident";

        [MFValueList(Required = true)]
        public MFIdentifier Resident_GendersValueList = "MFiles.Valuelist.Genders";


    }
}
