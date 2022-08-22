using MFiles.VAF.Common;
using MFilesAPI;
using System;

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

        [PropertyCustomValue("MFiles.Property.TariffVariance")]
        public TypedValue SetTariffVariance(PropertyEnvironment env)
        {
            ResidentAutomaticValueService residentAutomaticValueService = new ResidentAutomaticValueService(Configuration);

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeFloating, residentAutomaticValueService.GetTariffVariance(env.ObjVerEx));

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.ResidentDetail")]
        public TypedValue SetResidentDetail(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            string surname = env.ObjVerEx.GetProperty(Configuration.Resident_Surname).GetValueAsLocalizedText();
            string gender = env.ObjVerEx.GetProperty(Configuration.Resident_GenderTitle).GetValueAsLocalizedText();
            string initial = env.ObjVerEx.GetProperty(Configuration.Resident_Initial).GetValueAsLocalizedText();
            string accomodationCalc = env.ObjVerEx.GetProperty(Configuration.Resident_AccomodationCalc).GetValueAsLocalizedText();
            int deceasedLookupID = env.ObjVerEx.HasValue(Configuration.Resident_DeceasedDeparted) ? env.ObjVerEx.GetLookupID(Configuration.Resident_DeceasedDeparted) : 0;

            string addDeceasedKeyword = deceasedLookupID == Configuration.DeceasedListItem.ID ? "- DECEASED" : "";

            string name = $"{surname}, {initial} {addDeceasedKeyword} ({gender}) {accomodationCalc}";

            calculated.SetValue(MFDataType.MFDatatypeText, name);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.DurationOfStayInFrailcare")]
        public TypedValue SetDurationInFrailcare(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();
            DateTime deceasedDate = DateTime.Parse(env.ObjVerEx.GetProperty(Configuration.Resident_DateDeceased).GetValueAsLocalizedText());
            DateTime admissionDate = DateTime.Parse(env.ObjVerEx.GetProperty(Configuration.Resident_DateAdmittedToFrailCare).GetValueAsLocalizedText());

            int deceasedLookupID = env.ObjVerEx.HasValue(Configuration.Resident_DeceasedDeparted) ? env.ObjVerEx.GetLookupID(Configuration.Resident_DeceasedDeparted) : 0;

            int numOfDays = (deceasedDate - admissionDate).Days;

            if (deceasedLookupID != Configuration.DeceasedListItem.ID)
                numOfDays = 0;

            calculated.SetValue(MFDataType.MFDatatypeInteger, numOfDays);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.DurationOfStayInFacility")]
        public TypedValue SetDurationInFacility(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();
            DateTime deceasedDate = DateTime.Parse(env.ObjVerEx.GetProperty(Configuration.Resident_DateDeceased).GetValueAsLocalizedText());
            DateTime admissionDate = DateTime.Parse(env.ObjVerEx.GetProperty(Configuration.Resident_DateAdmittedToFacility).GetValueAsLocalizedText());

            int deceasedLookupID = env.ObjVerEx.HasValue(Configuration.Resident_DeceasedDeparted) ? env.ObjVerEx.GetLookupID(Configuration.Resident_DeceasedDeparted) : 0;

            int numOfDays = (deceasedDate - admissionDate).Days;

            if (deceasedLookupID != Configuration.DeceasedListItem.ID)
                numOfDays = 0;

            calculated.SetValue(MFDataType.MFDatatypeInteger, numOfDays);
            return calculated;
        }
    }
}
