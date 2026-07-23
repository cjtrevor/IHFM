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
        public MFIdentifier BBS_SittingToStanding = "MFiles.Property.SittingToStanding";
        [MFPropertyDef]
        public MFIdentifier BBS_StandingUnsupported = "MFiles.Property.StandingUnsupported";
        [MFPropertyDef]
        public MFIdentifier BBS_SittingWithBackUnsupportedButFeetSupported = "MFiles.Property.SittingWithBackUnsupportedButFeetSupported";
        [MFPropertyDef]
        public MFIdentifier BBS_StandingToSitting = "MFiles.Property.StandingToSitting";
        [MFPropertyDef]
        public MFIdentifier BBS_Transfers = "MFiles.Property.Transfers";
        [MFPropertyDef]
        public MFIdentifier BBS_StandingUnsupportedWithEyesClose = "MFiles.Property.StandingUnsupportedWithEyesClose";
        [MFPropertyDef]
        public MFIdentifier BBS_StandingUnsupportedWithFeetTogether = "MFiles.Property.StandingUnsupportedWithFeetTogether";
        [MFPropertyDef]
        public MFIdentifier BBS_ReachingForwardWithOutstretchedArmWhileStanding = "MFiles.Property.ReachingForwardWithOutstretchedArmWhileStanding";
        [MFPropertyDef]
        public MFIdentifier BBS_PickUpObjectFromTheFloorFromStandingPosition = "MFiles.Property.PickUpObjectFromTheFloorFromStandingPosition";
        [MFPropertyDef]
        public MFIdentifier BBS_TurningToLookBehindBothShouldersWhileStanding = "MFiles.Property.TurningToLookBehindBothShouldersWhileStanding";
        [MFPropertyDef]
        public MFIdentifier BBS_Turn360Degrees = "MFiles.Property.Turn360Degrees";
        [MFPropertyDef]
        public MFIdentifier BBS_PlaceFootOnStepstoolWhileStandingUnsupported = "MFiles.Property.PlaceFootOnStepstoolWhileStandingUnsupported";
        [MFPropertyDef]
        public MFIdentifier BBS_StandingUnsupportedOneFootInFront = "MFiles.Property.StandingUnsupportedOneFootInFront";
        [MFPropertyDef]
        public MFIdentifier BBS_StandingOnOneLeg = "MFiles.Property.StandingOnOneLeg";


        [MFPropertyDef]
        public MFIdentifier FRAT_Age = "MFiles.Property.Agefrat";
        [MFPropertyDef]
        public MFIdentifier FRAT_FallHistory = "MFiles.Property.FallHistory";
        [MFPropertyDef]
        public MFIdentifier FRAT_EliminationBowelAndUrine = "MFiles.Property.EliminationBowelAndUrine";
        [MFPropertyDef]
        public MFIdentifier FRAT_MedicationsInUse = "MFiles.Property.MedicationsInUse";
        [MFPropertyDef]
        public MFIdentifier FRAT_EquipmentThatTethersPatient = "MFiles.Property.EquipmentThatTethersPatient";
        [MFPropertyDef]
        public MFIdentifier FRAT_Mobility = "MFiles.Property.Mobilityfrat";
        [MFPropertyDef]
        public MFIdentifier FRAT_Cognition = "MFiles.Property.Cognitionfrat";
    }
}
