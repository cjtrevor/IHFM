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
        //Object Aliases
        [MFObjType(Required = true)]
        public MFIdentifier TranspharmStockObject = "MFiles.Object.TranspharmStock";

        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier TranspharmStockIssueQty = "MFiles.Property.Qty";

        [MFPropertyDef(Required = true)]
        public MFIdentifier OverrideStartTime = "MFiles.Property.OverrideStartTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier StartTimeTBC = "Mfiles.Property.StartTimeTbc";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ResidentLookup = "MFiles.Property.Resident";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TBCADLLookup = "MFiles.Property.TBCADL";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyADLLookup = "MFiles.Property.EveryDayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier WeekdaysADLLookup = "MFiles.Property.WeekdaysTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MondayADLLookup = "MFiles.Property.MondayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier TuesdayADLLookup = "MFiles.Property.TuesdayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier WednesdayADLLookup = "MFiles.Property.WednesdayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier ThursdayADLLookup = "MFiles.Property.ThursdayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier FridayADLLookup = "MFiles.Property.FridayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier SaturdayADLLookup = "MFiles.Property.SaturdayTBCADL";
        [MFPropertyDef(Required = true)]
        public MFIdentifier SundayADLLookup = "MFiles.Property.SundayTBCADL";
        
        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyClinicLookup = "MFiles.Property.EveryDayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier WeekdaysClinicLookup = "MFiles.Property.WeekdaysTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MondayClinicLookup = "MFiles.Property.MondayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier TuesdayClinicLookup = "MFiles.Property.TuesdayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier WednesdayClinicLookup = "MFiles.Property.WednesdayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier ThursdayClinicLookup = "MFiles.Property.ThursdayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier FridayClinicLookup = "MFiles.Property.FridayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier SaturdayClinicLookup = "MFiles.Property.SaturdayTBCClinic";
        [MFPropertyDef(Required = true)]
        public MFIdentifier SundayClinicLookup = "MFiles.Property.SundayTBCClinic";
        
        [MFPropertyDef(Required = true)]
        public MFIdentifier TBCClinicLookup = "MFiles.Property.TBCClinic";

        [MFPropertyDef(Required = true)]
        public MFIdentifier EndTime = "MFiles.Property.EndTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TimeSpent = "MFiles.Property.TimeSpent";

        [MFPropertyDef(Required = true)]
        public MFIdentifier CostForService = "MFiles.Property.CostForService";

        [MFPropertyDef(Required = true)]
        public MFIdentifier AverageTime = "MFiles.Property.AverageTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier AverageCost = "MFiles.Property.AverageCost";

        [MFPropertyDef]
        public MFIdentifier TBCS_TimeBasedCareItem = "MFiles.Property.TimeBasedCareItem";
        [MFPropertyDef]
        public MFIdentifier TBCS_TbcScheduledTimes = "MFiles.Property.TbcScheduledTimes";
    }
}
