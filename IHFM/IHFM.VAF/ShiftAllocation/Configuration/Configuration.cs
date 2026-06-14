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

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_CalendarCategory = "MFiles.Property.CalendarCategory";

        public const string CalendarCategory_DuduzileGUID = "{1A45FF1F-3D9E-4E0C-AE14-21F0209DD4FD}";
        public const string CalendarCategory_GavinGUID = "{71B54C7E-5E43-4A90-918B-84CFC7889E76}";
        public const string CalendarCategory_BevGUID = "{1A4DB7D2-179F-4548-922F-B10D96056D1C}";
        public const string CalendarCategory_DaylaGUID = "{30370B01-90D0-4CF8-9D22-72D4DA099C44}";
        public const string CalendarCategory_GeoffreyGUID = "{E79B1BCF-7DE2-444E-AD02-4F42EAF2CBF8}";
        public const string CalendarCategory_GeorgeSubGUID = "{9F2069D6-8B2D-4BAB-B648-70BEA8B596EA}";
        public const string CalendarCategory_IlanaGUID = "{07C50040-6528-467B-A256-1E6D46CEB994}";
        public const string CalendarCategory_JarredGUID = "{19186E59-32B9-43A8-B76D-46261B209E46}";
        public const string CalendarCategory_JolandeGUID = "{951CD30F-2443-4851-93B5-34CF60F7BB27}";
        public const string CalendarCategory_KatGUID = "{35CF0573-CD71-4802-B8E0-F51C42AAD47E}";
        public const string CalendarCategory_LizzieGUID = "{00E3E4A1-C5FC-4A26-980A-5D2095B0E607}";
        public const string CalendarCategory_MandyGUID = "{23176384-DDB6-4F52-8BB1-7E9F156A4ED2}";
        public const string CalendarCategory_MellisaGUID = "{74B89327-6B57-4EDD-8581-FBBF0FDEE034}";
        public const string CalendarCategory_SharleneGUID = "{F5D8F5B6-5E2D-4CC1-925C-433C7CC568A9}";
        public const string CalendarCategory_TrustGUID = "{E5BCFE73-6334-41F8-BA38-E7FD9A5D1665}";
        public const string CalendarCategory_CancellationGUID = "{6368FD41-666C-4D84-954D-F7A28356971C}";
        public const string CalendarCategory_PurpleCategoryGUID = "{7DFA5957-A18A-42C4-9C99-614ACF299E9A}";

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
