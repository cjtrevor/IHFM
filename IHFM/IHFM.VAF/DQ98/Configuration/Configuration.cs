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
        [MFPropertyDef]
        public MFIdentifier DQ98_A_PressureCare = "MFiles.Property.PressureCareDq98";
        [MFPropertyDef]
        public MFIdentifier DQ98_B_SpecialisedCare = "MFiles.Property.SpecialisedCareDq98";
        [MFPropertyDef]
        public MFIdentifier DQ98_C_NightCare = "MFiles.Property.NightcareDq98";
        [MFPropertyDef]
        public MFIdentifier DQ98_Total_A_B_C = "MFiles.Property.TotalScoreAbcDq98";


        [MFPropertyDef]
        public MFIdentifier DQ98_EatingADL = "MFiles.Property.EatingAdl";
        [MFPropertyDef]
        public MFIdentifier DQ98_DressingUpperADL = "MFiles.Property.DressingUpperAdl";
        [MFPropertyDef]
        public MFIdentifier DQ98_DressingLowerADL = "MFiles.Property.Dq98DressingLower";
        [MFPropertyDef]
        public MFIdentifier DQ98_PersonalHygieneADL = "MFiles.Property.Dq98PersonalHygiene";
        [MFPropertyDef]
        public MFIdentifier DQ98_BathingADL = "MFiles.Property.Dq98Bathing";
        [MFPropertyDef]
        public MFIdentifier DQ98_ToilettingADL = "MFiles.Property.Dq98Toileting";
        [MFPropertyDef]
        public MFIdentifier DQ98_MedicationsADL = "MFiles.Property.Dq98Medications";
        [MFPropertyDef]
        public MFIdentifier DQ98_MobilityADL = "MFiles.Property.Dq98Mobility";
        [MFPropertyDef]
        public MFIdentifier DQ98_CommunicationADL = "MFiles.Property.Dq98Communication";
        [MFPropertyDef]
        public MFIdentifier DQ98_TransfersADL = "MFiles.Property.Dq98Transfers";
        [MFPropertyDef]
        public MFIdentifier DQ98_TotalADL = "MFiles.Property.TotalScoreAdl";

        [MFPropertyDef]
        public MFIdentifier DQ98_MentalFunctioning = "MFiles.Property.MentalFunctioningDq98";
        [MFPropertyDef]
        public MFIdentifier DQ98_TotalMentalFunctioning = "MFiles.Property.TotalScoreMentalFunctioning";

        [MFPropertyDef]
        public MFIdentifier DQ98_WaterPrimaryNeed = "MFiles.Property.WaterPrimaryNeed";
        [MFPropertyDef]
        public MFIdentifier DQ98_FoodPrimaryNeed = "MFiles.Property.FoodPrimaryNeed";
        [MFPropertyDef]
        public MFIdentifier DQ98_ToiletPrimaryNeed = "MFiles.Property.ToiletPrimaryNeed";
        [MFPropertyDef]
        public MFIdentifier DQ98_SafetyPrimaryNeed = "MFiles.Property.SafetyPrimaryNeeds";
        [MFPropertyDef]
        public MFIdentifier DQ98_TotalPrimaryNeeds = "MFiles.Property.TotalScorePriimaryNeeds";

        [MFPropertyDef]
        public MFIdentifier DQ98_SupportSystemsAvailable = "PD.SupportSystemsAvailableToClient";
        [MFPropertyDef]
        public MFIdentifier DQ98_TotalSupportSystems = "MFiles.Property.TotalScoreSupportSystems";

        [MFPropertyDef]
        public MFIdentifier DQ98_GeneralCarerFunctioning = "MFiles.Property.GeneralFunctioningOfCarer";
        [MFPropertyDef]
        public MFIdentifier DQ98_TotalCarerFunctioning = "MFiles.Property.TotalScoreCarerFunctioning";

        [MFPropertyDef]
        public MFIdentifier DQ98_TotalSupportAndCarer = "MFiles.Property.TotalScoreSupportCarer";

        [MFPropertyDef]
        public MFIdentifier DQ98_Index_Score = "MFiles.Property.Dq98IndexScore";

    }
}
