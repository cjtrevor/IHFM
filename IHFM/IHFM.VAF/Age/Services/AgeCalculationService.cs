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
			DateTime birthDate;

			if (yearPart > currentYearPart)
			{
				birthDate = new DateTime(1900 + yearPart, monthPart, dayPart);
			}
			else
			{
				birthDate = new DateTime(2000 + yearPart, monthPart, dayPart);
			}

			int YearsPassed = DateTime.Now.Year - birthDate.Year;
			// Are we before the birth date this year? If so subtract one year from the mix
			if (DateTime.Now.Month < birthDate.Month || (DateTime.Now.Month == birthDate.Month && DateTime.Now.Day < birthDate.Day))
			{
				YearsPassed--;
			}
			return YearsPassed.ToString();
		}

		public void RefreshAge(ObjVerEx objVerEx, Configuration configuration, bool doCheckout = true)
        {
			string idNumber = objVerEx.GetPropertyText(configuration.IDNumber);
			if (!IdNumberParser.ValidateIDNumber(idNumber))
				return;
						
			string age = CalculateAge(idNumber);

			if(doCheckout)
				objVerEx.CheckOut();
			
			objVerEx.SetProperty(configuration.Age, MFilesAPI.MFDataType.MFDatatypeText, age);
			objVerEx.SaveProperties();
			
			if(doCheckout)
				objVerEx.CheckIn();
        }

		
	}
}
