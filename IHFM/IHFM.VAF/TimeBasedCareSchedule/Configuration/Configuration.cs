using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef]
        public MFIdentifier TBCS_Assistant = "MFiles.Property.Assistant";

        [MFPropertyDef]
        public MFIdentifier TBCS_TimeBasedCareItem = "MFiles.Property.TimeBasedCareItem";
        [MFPropertyDef]
        public MFIdentifier TBCS_TimeBasedCareItemItemName = "MFiles.Property.ItemName";
        [MFPropertyDef]
        public MFIdentifier TBCS_TbcScheduledTimes = "MFiles.Property.TbcScheduledTimes";

        [MFPropertyDef]
        public MFIdentifier TBCI_TBCType = "PD.Tbctype";

        [MFPropertyDef]
        public MFIdentifier TBCS_Frequency = "MFiles.Property.Frequency";

        public const string ScheduleFrequency_DaysOfWeekGUID = "{DC9F755B-24E9-4679-BBB6-9ABCDB223CAD}";
    }
}
