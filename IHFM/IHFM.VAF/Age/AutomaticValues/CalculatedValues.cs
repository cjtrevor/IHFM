using IHFM.VAF.Utilities;
using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Globalization;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.DobDay")]
        public TypedValue SetDobDayValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            string idNumber = "";

            if(env.ObjVerEx.Class == Configuration.Age_ResidentClass)
            {
                idNumber = env.ObjVerEx.GetPropertyText(Configuration.IDNumber);
            }
            else if(env.ObjVerEx.Class == Configuration.Age_StaffClass)
            {
                idNumber = env.ObjVerEx.GetPropertyText(Configuration.StaffIDNumber);
            }

            if (idNumber.Length < 6 || !IdNumberParser.ValidateIDNumber(idNumber))
            {
                return calculated;
            }

            calculated.SetValue(MFDataType.MFDatatypeText, IdNumberParser.GetDayPartFromIDNumber(idNumber));
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.DobMonth")]
        public TypedValue SetDobMonthValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            string idNumber = "";

            if (env.ObjVerEx.Class == Configuration.Age_ResidentClass)
            {
                idNumber = env.ObjVerEx.GetPropertyText(Configuration.IDNumber);
            }
            else if (env.ObjVerEx.Class == Configuration.Age_StaffClass)
            {
                idNumber = env.ObjVerEx.GetPropertyText(Configuration.StaffIDNumber);
            }

            if (idNumber.Length < 6 || !IdNumberParser.ValidateIDNumber(idNumber))
            {
                return calculated;
            }

            int monthPart = IdNumberParser.GetMonthPartFromIDNumber(idNumber);

            if(monthPart < 1 || monthPart > 12)
            {
                return calculated;
            }

            calculated.SetValue(MFDataType.MFDatatypeText, CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthPart));
            return calculated;
        }
    }
}
