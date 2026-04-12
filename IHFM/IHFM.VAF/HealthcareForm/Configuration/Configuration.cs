using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFClass]
        public MFIdentifier ConsentToMedia_Class = "MFiles.Class.ConsentToVideopictureRelease";
        [MFClass]
        public MFIdentifier RestraintRecord_Class = "MFiles.Class.RestraintRecord";
        [MFClass]
        public MFIdentifier ExclusionOfLI_Class = "MFiles.Class.ExclusionOfLiabilityAndIndemnity";

        [MFPropertyDef]
        public MFIdentifier HealthcareForm_Relationship = "MFiles.Property.Relationship";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_OnBehalfOfResident = "MFiles.Property.OnBehalfOfResidentNamesurname";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_CreatedBy = "MFiles.Property.CreatedBy";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_Date = "MFiles.Property.Date";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_FamilyPermissionDate = "MFiles.Property.FamilyPermissionDate";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_CareManagerAcknowledgement = "MFiles.Property.IAcknowledgeBeingInformedByTheCareManagersupervisorgpOfTheFollowingRisks";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_ProposedSolutionsAcknowledgement = "MFiles.Property.TheFollowingSolutionsWaswereProposedAndIsareUnacceptableToMe";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_NextOfKin = "MFiles.Property.NextOfKin";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_NextOfKinIdNumber = "MFiles.Property.NokIdNumber";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_NextOfKinCellNumber = "MFiles.Property.NokCellphoneNumber";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_RestraintType = "MFiles.Property.TypeOfRestraint";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_ReasonForResident = "MFiles.Property.ReasonForRestraint";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_PermissionFromFamilyMember = "MFiles.Property.PermissionFromFamilyMember";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_DoctorPermissionDate = "MFiles.Property.DrPermissionToRestrainDate";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_RestraintsPermitted = "MFiles.Property.WhenRestraintsArePermitted";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_RestrictionPeriodsAllowed = "MFiles.Property.PeriodsOfTimeResidentMayBeRestricted";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_RiskFactors = "MFiles.Property.RiskFactors";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_AssociatedRiskFactors = "MFiles.Property.AssociatedRiskFactors";
        [MFPropertyDef]
        public MFIdentifier HealthcareForm_CommentsNotes = "MFiles.Property.CommentsNotes";
    }
}
