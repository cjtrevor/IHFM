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
        public MFIdentifier DeathReport_DeathReportTitle = "PD.Deathreport";
        [MFPropertyDef]
        public MFIdentifier DeathReport_ResidentAll = "MFiles.Property.ResidentAll";
        [MFPropertyDef]
        public MFIdentifier DeathReport_ChooseProgressNote = "MFiles.Property.ChooseProgressNote";
        [MFPropertyDef]
        public MFIdentifier DeathReport_InResidence = "PD.InResidence";
        [MFPropertyDef]
        public MFIdentifier DeathReport_InFrailCare = "PD.InFrailCare";
        [MFPropertyDef]
        public MFIdentifier DeathReport_DateLastSeenByDr = "PD.DateLastSeenByDr";
        [MFPropertyDef]
        public MFIdentifier DeathReport_CircumstancesOfDeath = "PD.CircumstancesOfDeath";
        [MFPropertyDef]
        public MFIdentifier DeathReport_RoutineMedicationUsedPriorToDeath = "PD.RoutineMedicationUsedPriorToDeath";
        [MFPropertyDef]
        public MFIdentifier DeathReport_DrugsGivenThe4HoursPriorToDeath = "MFiles.Property.DrugsGivenThe4HoursPriorToDeath";
        [MFPropertyDef]
        public MFIdentifier DeathReport_FinalDisease = "PD.FinalDisease";
        [MFPropertyDef]
        public MFIdentifier DeathReport_ContributoryCauses = "PD.ContributoryCauses";
        [MFPropertyDef]
        public MFIdentifier DeathReport_UnderlyingCauses = "PD.UnderlyingCauses";
        [MFPropertyDef]
        public MFIdentifier DeathReport_CertifiedByDr = "PD.CertifiedByDr";
        [MFPropertyDef]
        public MFIdentifier DeathReport_DeathReportCompletedBySr = "PD.DeathReportCompletedBySr";
        [MFPropertyDef]
        public MFIdentifier DeathReport_CommentsNotes = "MFiles.Property.CommentsNotes";
        [MFPropertyDef]
        public MFIdentifier DeathReport_DurationOfIllness = "MFiles.Property.DurationOfIllness";
    }
}
