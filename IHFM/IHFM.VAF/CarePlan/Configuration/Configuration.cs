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
        [MFObjType(Required = true)]
        public MFIdentifier CarePlanObject = "MFiles.Object.CarePlan";

        public MFIdentifier CarePlanClass = "MFiles.Class.CarePloon";

        [MFPropertyDef]
        public MFIdentifier Careplan_CpDietAndFeeding = "MFiles.Property.CpDietAndFeeding";
        [MFPropertyDef]
        public MFIdentifier Careplan_CpToilet = "MFiles.Property.CpToilet";
        [MFPropertyDef]
        public MFIdentifier Careplan_CpPsychosocialSummary = "MFiles.Property.CpPsychosocialSummary";
        [MFPropertyDef]
        public MFIdentifier Careplan_CpWalkingAids = "MFiles.Property.CpWalkingAids";

        [MFPropertyDef]
        public MFIdentifier Careplan_EventsActivitiesOnceOff = "MFiles.Property.ActivitieseventsOnceoff";
        [MFPropertyDef]
        public MFIdentifier Careplan_EventsActivitiesDaily = "MFiles.Property.ActivitieseventsDaily";
        [MFPropertyDef]
        public MFIdentifier Careplan_EventsActivitiesWeekly = "MFiles.Property.ActivitieseventsWeekly";
        [MFPropertyDef]
        public MFIdentifier Careplan_EventsActivitiesMonthly = "MFiles.Property.ActivitieseventsMonthly";

    }
}
