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


        public const string Email_RecurrenceInterval_WeeklyGUID = "{810B4E9C-CA2D-4A96-99B2-D4CADCA27993}";
        public const string Email_RecurrenceInterval_BiWeeklyGUID = "{52C3E143-D492-4D93-A5E4-45A95F8D5E39}";
        public const string Email_RecurrenceInterval_TriWeeklyGUID = "{861978A1-3A9F-4078-ACFB-28AD9CBFED69}";

        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceInterval")]
        public MFIdentifier Email_RecurrenceInterval_Weekly = Email_RecurrenceInterval_WeeklyGUID;
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceInterval")]
        public MFIdentifier Email_RecurrenceInterval_BiWeekly = Email_RecurrenceInterval_BiWeeklyGUID;
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceInterval")]
        public MFIdentifier Email_RecurrenceInterval_TriWeekly = Email_RecurrenceInterval_TriWeeklyGUID;

        [DataMember]
        public string ShiftAllocation_MailLising { get; set; }
    }
}
