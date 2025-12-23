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
        public MFIdentifier ShiftAllocation_DayOfMonth = "MFiles.Property.DayOfMonth2";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_DaysOfWeek = "MFiles.Property.DaysOfWeek";

        [MFPropertyDef]
        public MFIdentifier ShiftAllocation_RecurrenceEndDate = "MFiles.Property.RecurrenceEndDate";


        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceFrequincy")]
        public MFIdentifier EmailFrequency_Daily = "{B4DFC279-35D6-47F3-BC41-850473B5A918}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceFrequincy")]
        public MFIdentifier EmailFrequency_Weekly = "{3FB5767D-13FF-4529-8E29-396A3777DCFD}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.RecurrenceFrequincy")]
        public MFIdentifier EmailFrequency_Monthly = "{D203262F-87C1-4205-943D-BF8F1B6D7469}";

        [DataMember]
        public string ShiftAllocation_MailLising { get; set; }
    }
}
