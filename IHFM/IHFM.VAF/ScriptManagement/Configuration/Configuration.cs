using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Property Definitions

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsOnScript = "Mfiles.Property.MedsOnScript";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds0200 = "Mfiles.Property.0200";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds0600 = "Mfiles.Property.0600?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds0900 = "Mfiles.Property.0900?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds1200 = "Mfiles.Property.1200?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds1400 = "Mfiles.Property.1400";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds1700 = "Mfiles.Property.1700?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds2000 = "Mfiles.Property.2000?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds2200 = "Mfiles.Property.2200";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ScriptManagementStartDate = "MFiles.Property.ScriptManagementStartDate";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ScriptManagementEndDate = "MFiles.Property.ScriptManagementEndDate";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ScriptManagement_Discontinued = "MFiles.Property.Discontinued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ScriptManagement_ScriptVerifiedCorrect = "MFiles.Property.ScriptVerifiedCorrect";

        [MFPropertyDef(Required = true)]
        public MFIdentifier PRNMedication = "MFiles.Property.PrnMedication";

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedicineList = "MFiles.Property.MedicineList";

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsDosageProperty = "MFiles.Property.MedsDosageProperty";

        [MFPropertyDef(Required = true)]
        public MFIdentifier QtyDispensed = "MFiles.Property.QtyDispensed";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SpecificDays = "MFiles.Property.SpecificDays";

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsDosage_SpecificDayOfMonth = "MFiles.Property.DayOfMonth";

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsDosage_4Hourly = "MFiles.Property.4Hourly";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsDosage_6Hourly = "MFiles.Property.6Hourly";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsDosage_StartTimeOf4HourlyCycle = "MFiles.Property.StartTimeOf4HourCycle";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DaysOfWeek = "MFiles.Property.DaysOfWeek";

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsTaken = "MFiles.Property.MedsTaken";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ScriptControl_MissedMeds = "MFiles.Property.MissedMeds";
        
        [MFPropertyDef(Required = true)]
        public MFIdentifier Provider = "MFiles.Property.Provider";

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsGiven_Adhoc = "MFiles.Property.Adhoc";

        //Class Definitions
        [MFClass(Required = true)]
        public MFIdentifier ScriptManagementClass = "MFiles.Class.ScriptManagement";

        [MFClass(Required = true)]
        public MFIdentifier MedsGiven = "MFiles.Class.MedsGiven";

        //Admin Configurations

        [DataMember]
        [JsonConfIntegerEditor(DefaultValue = 30, HelpText = "The amount of time to allow before/after a scheduled med slot to be included in meds given.")]
        public int ScriptControlTimeThreshold { get; set; }
    }
}
