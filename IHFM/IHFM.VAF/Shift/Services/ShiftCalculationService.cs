using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public class ShiftCalculationService
    {
        public string CalculateShiftNumber(ObjVerEx objVerEx)
        {
            string dateNow = objVerEx.GetPropertyText(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated);
            DateTime shiftStartTime = DateTime.Now; //Lookup from site object
            DateTime created = DateTime.Parse(dateNow);

            int shiftStartHour = shiftStartTime.Hour;
            int createdHour = created.Hour + 2; //Created is in UTC time

            string yearPart = created.Year.ToString().Substring(2, 2);
            string monthPart = "";
            string dayPart = "";
            string ShiftIndicator = "";

            if (createdHour < shiftStartHour)
            {
                monthPart = (created.AddDays(-1).Month).ToString().PadLeft(2, '0');
            }
            else
            {
                monthPart = (created.Month).ToString().PadLeft(2, '0');
            }

            if (createdHour < shiftStartHour)
            {
                dayPart = (created.AddDays(-1).Day).ToString().PadLeft(2, '0');
            }
            else
            {
                dayPart = (created.Day).ToString().PadLeft(2, '0');
            }

            if(createdHour >= shiftStartHour && createdHour < shiftStartHour + 12)
            {
                ShiftIndicator = "Day";
            }
            else if(createdHour >= shiftStartHour + 12 || createdHour < shiftStartHour)
            {
                ShiftIndicator = "Night";
            }

            return $"{dayPart}{monthPart}{yearPart}-{ShiftIndicator}";
        }
    }
}
