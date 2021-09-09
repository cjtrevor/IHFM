using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
		[PropertyCustomValue("MFiles.Property.Age")]
		public TypedValue ResidentAgeCustomValue(PropertyEnvironment env)

		{
			var ageValue = new TypedValue();

			string idNumber = env.ObjVerEx.Properties.SearchForProperty(Configuration.IDNumber.ID).GetValueAsLocalizedText();

			ageValue.SetValue(MFDataType.MFDatatypeText, CalculateAge(idNumber));
			return ageValue;
		}

		private string CalculateAge(string idNumber)
		{
			int yearPart = IdNumberParser.GetYearPartFromIDNumber(idNumber);
			int monthPart = IdNumberParser.GetMonthPartFromIDNumber(idNumber);
			int dayPart = IdNumberParser.GetDayPartFromIDNumber(idNumber);

			int currentYearPart = IdNumberParser.GetCurrentYearPart();

			if (yearPart > currentYearPart)
			{
				DateTime birthDate = new DateTime(1900 + yearPart, monthPart, dayPart);
				return ((DateTime.Now - birthDate).TotalDays / 365).ToString("N0");
			}
			else
			{
				DateTime birthDate = new DateTime(2000 + yearPart, monthPart, dayPart);
				return ((DateTime.Now - birthDate).TotalDays / 365).ToString("N0");
			}
		}
	}
}
