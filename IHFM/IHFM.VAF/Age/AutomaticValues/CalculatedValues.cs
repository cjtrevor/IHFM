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
			AgeCalculationService ageCalculationService = new AgeCalculationService();
			var ageValue = new TypedValue();

			string idNumber = env.ObjVerEx.Properties.SearchForProperty(Configuration.IDNumber.ID).GetValueAsLocalizedText();
			string age;

			if (string.IsNullOrEmpty(idNumber))
				age = "";
			else
				age = ageCalculationService.CalculateAge(idNumber);

			ageValue.SetValue(MFDataType.MFDatatypeText, age);
			return ageValue;
		}
	}
}
