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
        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentReport_InvestigationDone = "MFiles.Property.InvestigationDone";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentReport_RegardingIncident = "MFiles.Property.IncidentReported";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Cause = "MFiles.Property.ProbableRootcauseOfIncident";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_PreventativeActions = "MFiles.Property.PreventativeActions";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Comments = "MFiles.Property.AdditionalComments";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_ChronicPRNLast12 = "MFiles.Property.MedsGivenInLast12Hours";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_AssistiveDevices = "MFiles.Property.DoesResidentUseAssistiveDevice";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_AdditionalEquipment = "MFiles.Property.OtherEquipmentInUseAtTheTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Toiletted = "MFiles.Property.WasResidentToiletedBeforeIncident";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Glasses = "MFiles.Property.DoesResidentWearGlasses";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_GlassesDuring = "MFiles.Property.WasResidentWearingGlassesAtTheTime";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_HearingAid = "MFiles.Property.DoesResidentWearHearingAid";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_TiredOrUnwell = "MFiles.Property.WasResidentUnusuallyTiredunwell";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_MentalStateChange = "MFiles.Property.ChangeInMentalStatusLast90Days";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_MedicalDosageChange = "MFiles.Property.MedicationOrDosageChangedInLast30Days";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_IllnessPresent = "MFiles.Property.ShortTermacuteIllnessPresentEgUti";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_MedicationBruising = "MFiles.Property.IsResidentOnMedsWhichCauseBruising";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_AnythingOnFloor = "MFiles.Property.WasAnythingOnFloorWhichCouldHaveCausedIncident";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Lighting = "MFiles.Property.WasThereProperLighting";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Noise = "MFiles.Property.AnyUnusualLevelOfNoise";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Height = "MFiles.Property.WasBedchairAtProperHeight";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Reaching = "MFiles.Property.WasResidentReachingForItems";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Clothing = "MFiles.Property.WasResidentsClothingInTheWay";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Footwear = "MFiles.Property.WasResidentWearingProperFootwear";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_ChangeMedicalCondition = "MFiles.Property.RecentChangeInMedicalCondition";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Assessment = "MFiles.Property.WasEvaluationassessmentCompleted";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_ServicePlan = "MFiles.Property.WasTheResidentsCareservicePlanUpdated";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Physician = "MFiles.Property.WasTheResidentsSeenByPhysician";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_SimilarIncidents = "MFiles.Property.HasTheResidentsHadSimilarIncidents";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Reported = "MFiles.Property.IfNecessaryWasIncidentReportedToRegulatoryAgency";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Avoidable = "MFiles.Property.WasTheIncidentAvoidable";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_AbuseOrNeglect = "MFiles.Property.HowWasAbusenegletRuledOut";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_SuspectedCause = "MFiles.Property.SuspectedCause";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Date = "MFiles.Property.Date";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Time = "MFiles.Property.Time";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_LocationOfIncident = "MFiles.Property.Location";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_InterviewConducted = "MFiles.Property.WereInterviewsConducted";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Interviewee = "MFiles.Property.IntervieweeName";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_Resident = "MFiles.Property.ResidentAll";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncidentInvestigation_CommentsNotes = "MFiles.Property.CommentsNotes"; 

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_PH = "MFiles.Property.Ph";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_Sg = "MFiles.Property.Sg";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_LEU = "MFiles.Property.Leu";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_NIT = "MFiles.Property.Nit";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_PRO = "MFiles.Property.Pro";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_GLU = "MFiles.Property.Glu";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_Ket = "MFiles.Property.Ke";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_UBG = "MFiles.Property.Ubg";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_Bil = "MFiles.Property.Bil";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_ERY = "MFiles.Property.Ery";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_Hb = "MFiles.Property.IncidentHb";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Incident_Abnormal = "MFiles.Property.Abnormal";
    }
}
