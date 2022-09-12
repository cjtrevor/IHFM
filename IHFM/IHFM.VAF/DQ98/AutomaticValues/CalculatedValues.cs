using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.TotalScoreAbcDq98", Priority = 100)]
        public TypedValue SetTotalScoreABCValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int pressureCare = PropertyParser.ExtractPropertyValue(env.ObjVerEx,Configuration.DQ98_A_PressureCare);
            int specialisedCare = PropertyParser.ExtractPropertyValue(env.ObjVerEx,Configuration.DQ98_B_SpecialisedCare);          
            int nightCare = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_C_NightCare);

            calculated.SetValue(MFDataType.MFDatatypeInteger, pressureCare + specialisedCare + nightCare);

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalScoreAdl", Priority = 100)]
        public TypedValue SetTotalScoreADL(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int eatingADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_EatingADL);
            int dressingUpperADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_DressingUpperADL);
            int dressingLowerADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_DressingLowerADL);
            int personalHygieneADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_PersonalHygieneADL);
            int bathingADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_BathingADL);
            int toilettingADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_ToilettingADL);
            int medicationsADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_MedicationsADL);
            int mobilityADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_MobilityADL);
            int communicationADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_CommunicationADL);
            int transfersADL = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_TransfersADL);

            calculated.SetValue(MFDataType.MFDatatypeInteger, eatingADL + dressingUpperADL + dressingLowerADL + personalHygieneADL + bathingADL + toilettingADL + medicationsADL + mobilityADL + communicationADL + transfersADL);

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalScoreMentalFunctioning", Priority = 100)]
        public TypedValue SetTotalScoreMentalFunctioning(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int mentalFunctioning = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_MentalFunctioning);

            calculated.SetValue(MFDataType.MFDatatypeInteger, mentalFunctioning);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalScorePriimaryNeeds", Priority = 100)]
        public TypedValue SetTotalScorePrimaryNeeds(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int waterNeed = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_WaterPrimaryNeed);
            int foodNeed = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_FoodPrimaryNeed);
            int toiletNeed = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_ToiletPrimaryNeed);
            int safetyNeed = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_SafetyPrimaryNeed);

            calculated.SetValue(MFDataType.MFDatatypeInteger, waterNeed + foodNeed + toiletNeed + safetyNeed);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalScoreSupportSystems", Priority = 100)]
        public TypedValue SetTotalScoreSupportSystems(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int supportSystems = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_SupportSystemsAvailable);

            calculated.SetValue(MFDataType.MFDatatypeInteger, supportSystems);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalScoreCarerFunctioning", Priority = 100)]
        public TypedValue SetTotalScoreCarerFunctioning(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();
            int generalCarerFunctioning = PropertyParser.ExtractPropertyValue(env.ObjVerEx, Configuration.DQ98_GeneralCarerFunctioning);

            calculated.SetValue(MFDataType.MFDatatypeInteger, generalCarerFunctioning);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalScoreSupportCarer", Priority = 15)]
        public TypedValue SetTotalScoreSupportCarer(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int supportTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_TotalSupportSystems).GetValue<int>();
            int carerTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_TotalCarerFunctioning).GetValue<int>();

            calculated.SetValue(MFDataType.MFDatatypeInteger, supportTotal + carerTotal);
            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.Dq98IndexScore",Priority = 10)]
        public TypedValue SetIndexScore(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int skilledTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_Total_A_B_C).GetValue<int>();
            double skilledPart = skilledTotal * 0.2;

            int adlTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_TotalADL).GetValue<int>();
            double adlPart = adlTotal * 0.25;

            int mentalTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_TotalMentalFunctioning).GetValue<int>();
            double mentalPart = mentalTotal * 1;

            int primaryTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_TotalPrimaryNeeds).GetValue<int>();
            double primaryPart = primaryTotal * 0.15;

            int carerTotal = env.ObjVerEx.GetProperty(Configuration.DQ98_TotalCarerFunctioning).GetValue<int>();
            double carerPart = carerTotal * 0.15;

            calculated.SetValue(MFDataType.MFDatatypeFloating, skilledPart + adlPart + mentalPart + primaryPart + carerPart);
            return calculated;
        }
    }
}
