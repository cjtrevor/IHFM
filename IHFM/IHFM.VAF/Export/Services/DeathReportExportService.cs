using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class DeathReportExportService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public DeathReportExportService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void ExportRecord(ObjVerEx deathReportRecord)
        {
            DatabaseConnector connector = new DatabaseConnector();

            string deathReportTitle = deathReportRecord.GetPropertyText(_configuration.DeathReport_DeathReportTitle);
            string inResidence = deathReportRecord.GetProperty(_configuration.DeathReport_InResidence).GetValueAsLocalizedText();
            string inFrailcare = deathReportRecord.GetProperty(_configuration.DeathReport_InFrailCare).GetValueAsLocalizedText();
            string dateLastSeenByDr = deathReportRecord.GetProperty(_configuration.DeathReport_DateLastSeenByDr).GetValueAsLocalizedText();
            string circumstancesOfDeath = deathReportRecord.GetProperty(_configuration.DeathReport_CircumstancesOfDeath).GetValueAsLocalizedText();
            string routineMedicationUsedPriorToDeath = deathReportRecord.GetProperty(_configuration.DeathReport_RoutineMedicationUsedPriorToDeath).GetValueAsLocalizedText();
            string drugsGivenThe4HoursPriorToDeath = deathReportRecord.GetProperty(_configuration.DeathReport_DrugsGivenThe4HoursPriorToDeath).GetValueAsLocalizedText();
            string finalDisease = deathReportRecord.GetProperty(_configuration.DeathReport_FinalDisease).GetValueAsLocalizedText();
            string contributoryCauses = deathReportRecord.GetProperty(_configuration.DeathReport_ContributoryCauses).GetValueAsLocalizedText();
            string underlyingCauses = deathReportRecord.GetProperty(_configuration.DeathReport_UnderlyingCauses).GetValueAsLocalizedText();         
            string certifiedByDr = deathReportRecord.GetProperty(_configuration.DeathReport_CertifiedByDr).GetValueAsLocalizedText();
            string deathReportCompletedBySr = deathReportRecord.GetProperty(_configuration.DeathReport_DeathReportCompletedBySr).GetValueAsLocalizedText();
            string commentsNotes = deathReportRecord.GetProperty(_configuration.DeathReport_CommentsNotes).GetValueAsLocalizedText();
            string durationOfIllness = deathReportRecord.GetProperty(_configuration.DeathReport_DurationOfIllness).GetValueAsLocalizedText();

            //Progress Note
            ObjVerEx progressNote = new ObjVerEx(_vault, deathReportRecord.GetProperty(_configuration.DeathReport_ChooseProgressNote).TypedValue.GetValueAsLookup());
            string progressNoteComments = progressNote.GetProperty(_configuration.DeathReport_CommentsNotes).GetValueAsLocalizedText();

            //Resident
            ObjVerEx resident = new ObjVerEx(_vault, deathReportRecord.GetProperty(_configuration.DeathReport_ResidentAll).TypedValue.GetValueAsLookup());
            string durationInFacility = resident.GetProperty(_configuration.Resident_DurationInFacility).GetValueAsLocalizedText();
            string durationInFrailcare = resident.GetProperty(_configuration.Resident_DurationInFrailcare).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportDeathReport";
            storedProc.storedProcParams = new Dictionary<string, object>();

            storedProc.storedProcParams.Add("@DeathReportTitle", deathReportTitle);
            storedProc.storedProcParams.Add("@ProgressNotesCommentsNotes", progressNoteComments);
            storedProc.storedProcParams.Add("@InResidence", inResidence);
            storedProc.storedProcParams.Add("@InFrailCare", inFrailcare);
            storedProc.storedProcParams.Add("@DateLastSeenByDr", dateLastSeenByDr);
            storedProc.storedProcParams.Add("@CircumstancesOfDeath", circumstancesOfDeath);

            storedProc.storedProcParams.Add("@RoutineMedicationUsedPriorToDeath", routineMedicationUsedPriorToDeath);
            storedProc.storedProcParams.Add("@DrugsGivenThe4HoursPriorToDeath", drugsGivenThe4HoursPriorToDeath);
            storedProc.storedProcParams.Add("@FinalDisease", finalDisease);
            storedProc.storedProcParams.Add("@ContributoryCauses", contributoryCauses);
            storedProc.storedProcParams.Add("@UnderlyingCauses", underlyingCauses);

            storedProc.storedProcParams.Add("@DurationInFrailcare", durationInFrailcare);
            storedProc.storedProcParams.Add("@DurationInFacility", durationInFacility);
            storedProc.storedProcParams.Add("@DurationOfIllness", durationOfIllness);
            storedProc.storedProcParams.Add("@CertifiedByDr", certifiedByDr);
            storedProc.storedProcParams.Add("@DeathReportCompletedBySr", deathReportCompletedBySr);
            storedProc.storedProcParams.Add("@CommentsNotes", commentsNotes);
            storedProc.storedProcParams.Add("@Created", DateTime.Now);

            connector.ExecuteStoredProc(storedProc);
        }
    }
}
