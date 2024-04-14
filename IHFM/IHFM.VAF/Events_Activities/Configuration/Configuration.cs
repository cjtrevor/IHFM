using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier Events_ActivitiesEventsClass = "MFiles.Class.ActivitieseventSchedule";

        //Property Aliases
        [MFPropertyDef]
        public MFIdentifier Events_EventSchedule = "MFiles.Property.EventSchedule";
        [MFPropertyDef]
        public MFIdentifier Events_ResidentsDropdown = "MFiles.Property.Residents";
        [MFPropertyDef]
        public MFIdentifier Events_Date = "MFiles.Property.Date";
        [MFPropertyDef]
        public MFIdentifier Events_OnceOnly = "MFiles.Property.OnceOnly";
        [MFPropertyDef]
        public MFIdentifier Events_Daily = "MFiles.Property.Daily";
        [MFPropertyDef]
        public MFIdentifier Events_Weekdays = "MFiles.Property.WeeklyDayOfWeek";
        [MFPropertyDef]
        public MFIdentifier Events_Month = "MFiles.Property.SpecificDayOfMonth";

        [MFPropertyDef]
        public MFIdentifier Attendance_WhichEvent = "MFiles.Property.WhichEvent";
        [MFPropertyDef]
        public MFIdentifier Attendance_Time = "MFiles.Property.Time";
        [MFPropertyDef]
        public MFIdentifier Attendance_ResidentsDropdown = "MFiles.Property.Residents";
        [MFPropertyDef]
        public MFIdentifier Attendance_Date = "MFiles.Property.Date";


    }
}
