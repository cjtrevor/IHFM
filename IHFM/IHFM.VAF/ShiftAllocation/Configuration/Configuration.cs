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
        [MFClass]
        public MFIdentifier ShiftAllocation_Class = "MFiles.Class.ShiftAllocation";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_Resident = "MFiles.Property.ResidentHbc";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_StartDateTime = "MFiles.Property.StartDateTime";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_EndDateTime = "MFiles.Property.EndDateTime";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_StaffAttending = "MFiles.Property.StaffAttending";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_RecurrenceFrequency = "MFiles.Property.RecurrenceFrequency";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_RecurrenceInterval = "MFiles.Property.RecurrenceInterval";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_DayOfMonth = "MFiles.Property.DayOfMonth2";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_DaysOfWeek = "MFiles.Property.DaysOfWeek";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_RecurrenceEndDate = "MFiles.Property.RecurrenceEndDate";


        public const string EmailFrequency_DailyGUID = "{B4DFC279-35D6-47F3-BC41-850473B5A918}";
        public const string EmailFrequency_WeeklyGUID = "{3FB5767D-13FF-4529-8E29-396A3777DCFD}";
        public const string EmailFrequency_MonthlyGUID = "{D203262F-87C1-4205-943D-BF8F1B6D7469}";

        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceFrequincy")]
        public MFIdentifier EmailFrequency_Daily = EmailFrequency_DailyGUID;
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceFrequincy")]
        public MFIdentifier EmailFrequency_Weekly = EmailFrequency_WeeklyGUID;
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceFrequincy")]
        public MFIdentifier EmailFrequency_Monthly = EmailFrequency_MonthlyGUID;


        public const string Email_RecurrenceInterval_WeeklyGUID = "{BB5E7AD8-7590-4E6A-8482-25E7D828AE6A}";
        public const string Email_RecurrenceInterval_BiWeeklyGUID = "{3B1178C6-69CC-4B58-B002-2F398C81E5C7}";
        public const string Email_RecurrenceInterval_TriWeeklyGUID = "{9665EC2F-19E4-44BE-AE08-F8E806EF1EF2}";

        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceInterval")]
        public MFIdentifier Email_RecurrenceInterval_Weekly = "{BB5E7AD8-7590-4E6A-8482-25E7D828AE6A}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceInterval")]
        public MFIdentifier Email_RecurrenceInterval_BiWeekly = Email_RecurrenceInterval_BiWeeklyGUID;
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceInterval")]
        public MFIdentifier Email_RecurrenceInterval_TriWeekly = Email_RecurrenceInterval_TriWeeklyGUID;

        [DataMember]
        public string ShiftAllocation_MailLising { get; set; }
    }
}
