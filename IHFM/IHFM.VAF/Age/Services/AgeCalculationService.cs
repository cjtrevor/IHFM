using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class AgeCalculationService
    {
		public string CalculateAge(string idNumber)
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

		public void RefreshAge(ObjVerEx objVerEx, Configuration configuration, bool doCheckout = true)
        {
			string idNumber = objVerEx.GetPropertyText(configuration.IDNumber);
			if (!ValidateIDNumber(idNumber))
				return;
						
			string age = CalculateAge(idNumber);

			if(doCheckout)
				objVerEx.CheckOut();
			
			objVerEx.SetProperty(configuration.Age, MFilesAPI.MFDataType.MFDatatypeText, age);
			objVerEx.SaveProperties();
			
			if(doCheckout)
				objVerEx.CheckIn();
        }

		private bool ValidateIDNumber(string idNumber)
        {
			if (string.IsNullOrEmpty(idNumber) && idNumber.Length != 13)
				return false;

			if (!Regex.IsMatch(idNumber, "^[0-9]*$"))
				return false;

			return true;
        }
	}
}
