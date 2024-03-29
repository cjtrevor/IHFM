﻿using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IHFM.VAF.Utilities;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("Mfiles.Property.Medsdosage", Priority = 1)]
        public TypedValue SetMedsDosageValue(PropertyEnvironment env)
        {
            string medicineList = env.ObjVerEx.GetProperty(Configuration.MedicineList).GetValueAsLocalizedText();
            string medsDosage = env.ObjVerEx.GetProperty(Configuration.MedsDosageProperty).GetValueAsLocalizedText();
            string qtyDispensed = env.ObjVerEx.GetProperty(Configuration.QtyDispensed).GetValueAsLocalizedText();
            string daysOfWeek = "on All days";
                        
            if(env.ObjVerEx.HasValue(Configuration.SpecificDays) && env.ObjVerEx.GetProperty(Configuration.SpecificDays).GetValue<bool>())
            { 
                daysOfWeek = "on " + env.ObjVerEx.GetProperty(Configuration.DaysOfWeek).GetValueAsLocalizedText();
            }

            if (env.ObjVerEx.HasValue(Configuration.MedsDosage_SpecificDayOfMonth))
            {
                string dayWithSuffix = (Int32.Parse(env.ObjVerEx.GetProperty(Configuration.MedsDosage_SpecificDayOfMonth).GetValueAsLocalizedText())).Ordinal();
                daysOfWeek = $" on the {dayWithSuffix} of the month";
            }

            if(env.ObjVerEx.HasValue(Configuration.MedsDosage_4Hourly) && env.ObjVerEx.GetProperty(Configuration.MedsDosage_4Hourly).GetValue<bool>())
            {
                daysOfWeek = $"every 4 hours starting at {env.ObjVerEx.GetProperty(Configuration.MedsDosage_StartTimeOf4HourlyCycle).GetValueAsLocalizedText()}";
            }

            string PRN = "";

            string timeslots = "";

            if (env.ObjVerEx.HasValue(Configuration.GiveMeds0600) && env.ObjVerEx.GetProperty(Configuration.GiveMeds0600).GetValue<bool>())
                timeslots += "06:00 | ";
            if (env.ObjVerEx.HasValue(Configuration.GiveMeds0900) && env.ObjVerEx.GetProperty(Configuration.GiveMeds0900).GetValue<bool>())
                timeslots += "09:00 | ";
            if (env.ObjVerEx.HasValue(Configuration.GiveMeds1200) && env.ObjVerEx.GetProperty(Configuration.GiveMeds1200).GetValue<bool>())
                timeslots += "12:00 | ";
            if (env.ObjVerEx.HasValue(Configuration.GiveMeds1700) && env.ObjVerEx.GetProperty(Configuration.GiveMeds1700).GetValue<bool>())
                timeslots += "17:00 | ";
            if (env.ObjVerEx.HasValue(Configuration.GiveMeds2000) && env.ObjVerEx.GetProperty(Configuration.GiveMeds2000).GetValue<bool>())
                timeslots += "20:00 | ";

            if (env.ObjVerEx.HasValue(Configuration.PRNMedication) && env.ObjVerEx.GetProperty(Configuration.PRNMedication).GetValue<bool>())
                PRN = "_PRN";

            string times = timeslots.Length > 0 ? $"@: {timeslots.Substring(0, timeslots.Length - 2)} {daysOfWeek}" : "";

            string name = $"{medicineList}_{medsDosage}{PRN} x {qtyDispensed} {times}";

            TypedValue calculated = new TypedValue();
            calculated.SetValue(MFDataType.MFDatatypeText, name);

            return calculated;
        }
    }
}
