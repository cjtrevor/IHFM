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
        [PropertyCustomValue("MFiles.Property.BbsTotalScore", Priority = 100)]
        public TypedValue SetTotalScoreBBSValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int sittingToStanding = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_SittingToStanding);
            int standingUnsupported = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_StandingUnsupported);
            int sittingWithBackUnsupportedButFeetSupported = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_SittingWithBackUnsupportedButFeetSupported);
            int standingToSitting = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_StandingToSitting);
            int transfers = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_Transfers);
            int standingUnsupportedWithEyesClose = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_StandingUnsupportedWithEyesClose);
            int standingUnsupportedWithFeetTogether = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_StandingUnsupportedWithFeetTogether);
            int reachingForwardWithOutstretchedArmWhileStanding = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_ReachingForwardWithOutstretchedArmWhileStanding);
            int pickUpObjectFromTheFloorFromStandingPosition = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_PickUpObjectFromTheFloorFromStandingPosition);
            int turningToLookBehindBothShouldersWhileStanding = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_TurningToLookBehindBothShouldersWhileStanding);
            int turn360Degrees = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_Turn360Degrees);
            int placeFootOnStepstoolWhileStandingUnsupported = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_PlaceFootOnStepstoolWhileStandingUnsupported);
            int standingUnsupportedOneFootInFront = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_StandingUnsupportedOneFootInFront);
            int standingOnOneLeg = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.BBS_StandingOnOneLeg);

            calculated.SetValue(MFDataType.MFDatatypeInteger, sittingToStanding + standingUnsupported + sittingWithBackUnsupportedButFeetSupported + standingToSitting + transfers + standingUnsupportedWithEyesClose + standingUnsupportedWithFeetTogether + reachingForwardWithOutstretchedArmWhileStanding + pickUpObjectFromTheFloorFromStandingPosition + turningToLookBehindBothShouldersWhileStanding + turn360Degrees + placeFootOnStepstoolWhileStandingUnsupported + standingUnsupportedOneFootInFront + standingOnOneLeg);

            return calculated;
        }

        [PropertyCustomValue("MFiles.Property.TotalFratScore", Priority = 100)]
        public TypedValue SetTotalScoreFRATValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            int age = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_Age);
            int fallHistory = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_FallHistory);
            int eliminationBowelAndUrine = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_EliminationBowelAndUrine);
            int medicationsInUse = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_MedicationsInUse);
            int equipmentThatTethersPatient = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_EquipmentThatTethersPatient);
            int mobility = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_Mobility);
            int cognition = PropertyParser.ExtractPropertyValueSquareBraces(env.ObjVerEx, Configuration.FRAT_Cognition);

            calculated.SetValue(MFDataType.MFDatatypeInteger, age + fallHistory + eliminationBowelAndUrine + medicationsInUse + equipmentThatTethersPatient + mobility + cognition);

            return calculated;
        }

    }
}
