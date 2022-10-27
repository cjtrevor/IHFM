using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class IncidentInvestigationExportService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;
        private readonly DatabaseConnector _databaseConnector;

        public IncidentInvestigationExportService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
            _databaseConnector = new DatabaseConnector();
        }

        public void ExportIncidentInvestigation(ObjVerEx investigation)
        {
            ExportSummary(investigation);
            ExportResidentSpecificFactors(investigation);
            ExportMedicalFactors(investigation);
            ExportIncidentReport(investigation);
            ExportGeneralInfo(investigation);
            ExportEnvironmentalFactors(investigation);
            ExportAdditionalInformation(investigation);
            ExportIncidentInvestigationDetails(investigation);

        }
        public void ExportSummary(ObjVerEx investigation)
        {
            string cause = investigation.GetProperty(_configuration.IncidentInvestigation_Cause).GetValueAsLocalizedText();
            string preventativeActions = investigation.GetProperty(_configuration.IncidentInvestigation_PreventativeActions).GetValueAsLocalizedText();
            string comments = investigation.GetProperty(_configuration.IncidentInvestigation_Comments).GetValueAsLocalizedText();
            string chronicPRNLast12 = investigation.GetProperty(_configuration.IncidentInvestigation_ChronicPRNLast12).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvSummaryExport";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@Cause", cause);
            storedProc.storedProcParams.Add("@PreventativeActions", preventativeActions);
            storedProc.storedProcParams.Add("@Comments", comments);
            storedProc.storedProcParams.Add("@ChronicPRNLast12", chronicPRNLast12);

            _databaseConnector.ExecuteStoredProc(storedProc);           
        }
        public void ExportResidentSpecificFactors(ObjVerEx investigation)
        {
            string assistedDevices = investigation.GetProperty(_configuration.IncidentInvestigation_AssistiveDevices).GetValueAsLocalizedText();
            string additionalEquipment = investigation.GetProperty(_configuration.IncidentInvestigation_AdditionalEquipment).GetValueAsLocalizedText();
            bool toiletted = investigation.HasValue(_configuration.IncidentInvestigation_Toiletted) ? investigation.GetProperty(_configuration.IncidentInvestigation_Toiletted).GetValue<bool>() : false;
            bool glasses = investigation.HasValue(_configuration.IncidentInvestigation_Glasses) ? investigation.GetProperty(_configuration.IncidentInvestigation_Glasses).GetValue<bool>() : false; ;
            bool glassesDuring = investigation.HasValue(_configuration.IncidentInvestigation_GlassesDuring) ? investigation.GetProperty(_configuration.IncidentInvestigation_GlassesDuring).GetValue<bool>() : false; ;
            bool hearingAid = investigation.HasValue(_configuration.IncidentInvestigation_HearingAid) ? investigation.GetProperty(_configuration.IncidentInvestigation_HearingAid).GetValue<bool>() : false; ;
            bool tiredOrUnwell = investigation.HasValue(_configuration.IncidentInvestigation_TiredOrUnwell) ? investigation.GetProperty(_configuration.IncidentInvestigation_TiredOrUnwell).GetValue<bool>() : false; ;

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvResidentSpecificFactors";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@AssistiveDevices", assistedDevices);
            storedProc.storedProcParams.Add("@AdditionalEquipment", additionalEquipment);
            storedProc.storedProcParams.Add("@Toiletted", toiletted);
            storedProc.storedProcParams.Add("@Glasses", glasses);
            storedProc.storedProcParams.Add("@GlassesDuring", glassesDuring);
            storedProc.storedProcParams.Add("@HearingAid", hearingAid);
            storedProc.storedProcParams.Add("@TiredOrUnwell", tiredOrUnwell);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
        public void ExportMedicalFactors(ObjVerEx investigation)
        {
             bool mentalStateChange = investigation.HasValue(_configuration.IncidentInvestigation_MentalStateChange) ? investigation.GetProperty(_configuration.IncidentInvestigation_MentalStateChange).GetValue<bool>() : false;
            bool medicalDosageChange = investigation.HasValue(_configuration.IncidentInvestigation_MedicalDosageChange) ? investigation.GetProperty(_configuration.IncidentInvestigation_MedicalDosageChange).GetValue<bool>() : false;
            bool illnessPresent = investigation.HasValue(_configuration.IncidentInvestigation_IllnessPresent) ? investigation.GetProperty(_configuration.IncidentInvestigation_IllnessPresent).GetValue<bool>() : false;
            bool medicationBruising = investigation.HasValue(_configuration.IncidentInvestigation_MedicationBruising) ? investigation.GetProperty(_configuration.IncidentInvestigation_MedicationBruising).GetValue<bool>() : false;
            string medicationLast12Hours = investigation.GetProperty(_configuration.IncidentInvestigation_ChronicPRNLast12).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvMedicalFactors";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@MentalStateChange", mentalStateChange);
            storedProc.storedProcParams.Add("@MedicalDosageChange", medicalDosageChange);
            storedProc.storedProcParams.Add("@IllnessPresent", illnessPresent);
            storedProc.storedProcParams.Add("@MedicationBruising", medicationBruising);
            storedProc.storedProcParams.Add("@MedicationLast12Hours", medicationLast12Hours);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
        public void ExportIncidentReport(ObjVerEx investigation)
        {
            Lookup incLookup = investigation.GetProperty(_configuration.IncidentReport_RegardingIncident).Value.GetValueAsLookup();
            ObjVerEx incident = new ObjVerEx(_vault, incLookup);
            
            Lookup resLookup = investigation.GetProperty(_configuration.IncidentInvestigation_Resident).Value.GetValueAsLookup();
            ObjVerEx residentObj = new ObjVerEx(_vault, resLookup);

            DateTime incidentDateTime = DateTime.Parse(incident.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText());
            string shift = incident.GetProperty(_configuration.Shift).GetValueAsLocalizedText();
            string temperature = incident.GetProperty(_configuration.Vitals_Temperature).GetValueAsLocalizedText();
            string systolicBP = incident.GetProperty(_configuration.Vitals_SystolicBP).GetValueAsLocalizedText();
            string diastolicBP = incident.GetProperty(_configuration.Vitals_DiastolicBP).GetValueAsLocalizedText();
            bool flaggedAbnormal = incident.HasValue(_configuration.Incident_Abnormal) ? incident.GetProperty(_configuration.Incident_Abnormal).GetValue<bool>() : false; ;
            string pulse = incident.GetProperty(_configuration.Vitals_HeartRate).GetValueAsLocalizedText();           
            string ph = incident.GetProperty(_configuration.Incident_PH).GetValueAsLocalizedText();
            string sg = incident.GetProperty(_configuration.Incident_Sg).GetValueAsLocalizedText();
            string LEU = incident.GetProperty(_configuration.Incident_LEU).GetValueAsLocalizedText();
            string NIT = incident.GetProperty(_configuration.Incident_NIT).GetValueAsLocalizedText();
            string PRO = incident.GetProperty(_configuration.Incident_PRO).GetValueAsLocalizedText();
            string GLU = incident.GetProperty(_configuration.Incident_GLU).GetValueAsLocalizedText();
            string Ket = incident.GetProperty(_configuration.Incident_Ket).GetValueAsLocalizedText();
            string UBG = incident.GetProperty(_configuration.Incident_UBG).GetValueAsLocalizedText();
            string bil = incident.GetProperty(_configuration.Incident_Bil).GetValueAsLocalizedText();
            string ERY = incident.GetProperty(_configuration.Incident_ERY).GetValueAsLocalizedText();
            string hb = incident.GetProperty(_configuration.Incident_Hb).GetValueAsLocalizedText();
            string commments = incident.GetProperty(_configuration.IncidentInvestigation_CommentsNotes).GetValueAsLocalizedText();

            DateTime dateOfDeath = residentObj.HasValue(_configuration.Resident_DateDeceased) ? DateTime.Parse(residentObj.GetProperty(_configuration.Resident_DateDeceased).GetValueAsLocalizedText()) : DateTime.Parse("01/01/1900");
            string residentDiagnosis = residentObj.GetProperty(_configuration.Resident_MedicalConditions).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvIncidentReport";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@IncidentId", incident.ID);
            storedProc.storedProcParams.Add("@IncidentDateTime", incidentDateTime);
            storedProc.storedProcParams.Add("@Shift", shift);
            storedProc.storedProcParams.Add("@Temperature", temperature);
            storedProc.storedProcParams.Add("@SystolicBP", systolicBP);
            storedProc.storedProcParams.Add("@DiastolicBP", diastolicBP);
            storedProc.storedProcParams.Add("@FlaggedAbnormal", flaggedAbnormal);
            storedProc.storedProcParams.Add("@Pulse", pulse);
            storedProc.storedProcParams.Add("@DateOfDeath", dateOfDeath);
            storedProc.storedProcParams.Add("@ResidentDiagnosis", residentDiagnosis);
            storedProc.storedProcParams.Add("@Ph", ph);
            storedProc.storedProcParams.Add("@Sg", sg);
            storedProc.storedProcParams.Add("@LEU", LEU);
            storedProc.storedProcParams.Add("@NIT", NIT);
            storedProc.storedProcParams.Add("@PRO", PRO);
            storedProc.storedProcParams.Add("@GLU", GLU);
            storedProc.storedProcParams.Add("@Ket", Ket);
            storedProc.storedProcParams.Add("@UBG", UBG);
            storedProc.storedProcParams.Add("@Bil", bil);
            storedProc.storedProcParams.Add("@ERY", ERY);
            storedProc.storedProcParams.Add("@Hb", hb);
            storedProc.storedProcParams.Add("@Comments", commments);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
        public void ExportGeneralInfo(ObjVerEx investigation)
        {
            int residentId = investigation.GetLookupID(_configuration.IncidentInvestigation_Resident);
            string resident = investigation.GetProperty(_configuration.IncidentInvestigation_Resident).GetValueAsLocalizedText();

            Lookup resLookup = investigation.GetProperty(_configuration.IncidentInvestigation_Resident).Value.GetValueAsLookup();
            ObjVerEx residentObj = new ObjVerEx(_vault, resLookup);

            string gender = residentObj.GetProperty(_configuration.Resident_GenderTitle).GetValueAsLocalizedText();
            string iDNum = residentObj.GetProperty(_configuration.Resident_IDNumber).GetValueAsLocalizedText();

            int siteId = residentObj.GetLookupID(_configuration.Resident_Site);
            string site = residentObj.GetProperty(_configuration.Resident_Site).GetValueAsLocalizedText();

            DateTime admissionDate; 
            if(!DateTime.TryParse(residentObj.GetProperty(_configuration.Resident_DateAdmittedToFacility).GetValueAsLocalizedText(),out admissionDate))
            {
                throw new Exception("The admission date on the resident is not valid. Please fix the admission date and save again.");
            }

            string CPOARef = residentObj.GetProperty(_configuration.Resident_CPOARef).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvGeneralInfo";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@ResidentId", residentId);
            storedProc.storedProcParams.Add("@Resident", resident);
            storedProc.storedProcParams.Add("@Gender", gender);
            storedProc.storedProcParams.Add("@IDNum", iDNum);
            storedProc.storedProcParams.Add("@SiteId", siteId);
            storedProc.storedProcParams.Add("@Site", site);
            storedProc.storedProcParams.Add("@AdmissionDate", admissionDate);
            storedProc.storedProcParams.Add("@CPOARef", CPOARef);

            _databaseConnector.ExecuteStoredProc(storedProc);


        }
        public void ExportEnvironmentalFactors(ObjVerEx investigation)
        {
            bool anythingOnFloor = investigation.HasValue(_configuration.IncidentInvestigation_AnythingOnFloor ) ? investigation.GetProperty(_configuration.IncidentInvestigation_AnythingOnFloor).GetValue<bool>() : false;
            bool lighting = investigation.HasValue(_configuration.IncidentInvestigation_Lighting) ? investigation.GetProperty(_configuration.IncidentInvestigation_Lighting).GetValue<bool>() : false;
            bool noise = investigation.HasValue(_configuration.IncidentInvestigation_Noise) ? investigation.GetProperty(_configuration.IncidentInvestigation_Noise).GetValue<bool>() : false;
            string height = investigation.GetProperty(_configuration.IncidentInvestigation_Height).GetValueAsLocalizedText();
            bool reaching = investigation.HasValue(_configuration.IncidentInvestigation_Reaching) ? investigation.GetProperty(_configuration.IncidentInvestigation_Reaching).GetValue<bool>() : false;
            bool clothing = investigation.HasValue(_configuration.IncidentInvestigation_Clothing) ? investigation.GetProperty(_configuration.IncidentInvestigation_Clothing).GetValue<bool>() : false;
            bool footwear = investigation.HasValue(_configuration.IncidentInvestigation_Footwear) ? investigation.GetProperty(_configuration.IncidentInvestigation_Footwear).GetValue<bool>() : false;

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvEnvironmentalFactors";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@AnythingOnFloor", anythingOnFloor);
            storedProc.storedProcParams.Add("@Lighting", lighting);
            storedProc.storedProcParams.Add("@Noise", noise);
            storedProc.storedProcParams.Add("@Height", height);
            storedProc.storedProcParams.Add("@Reaching", reaching);
            storedProc.storedProcParams.Add("@Clothing", clothing);
            storedProc.storedProcParams.Add("@Footwear", footwear);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
        public void ExportAdditionalInformation(ObjVerEx investigation)
        {
            bool changeMedicalCondition = investigation.HasValue(_configuration.IncidentInvestigation_ChangeMedicalCondition) ? investigation.GetProperty(_configuration.IncidentInvestigation_ChangeMedicalCondition).GetValue<bool>() : false;
            bool assessment = investigation.HasValue(_configuration.IncidentInvestigation_Assessment) ? investigation.GetProperty(_configuration.IncidentInvestigation_Assessment).GetValue<bool>() : false;
            bool servicePlan = investigation.HasValue(_configuration.IncidentInvestigation_ServicePlan) ? investigation.GetProperty(_configuration.IncidentInvestigation_ServicePlan).GetValue<bool>() : false;
            bool physician = investigation.HasValue(_configuration.IncidentInvestigation_Physician) ? investigation.GetProperty(_configuration.IncidentInvestigation_Physician).GetValue<bool>() : false;
            bool similarIncidents = investigation.HasValue(_configuration.IncidentInvestigation_SimilarIncidents) ? investigation.GetProperty(_configuration.IncidentInvestigation_SimilarIncidents).GetValue<bool>() : false;
            string reported = investigation.GetProperty(_configuration.IncidentInvestigation_Reported).GetValueAsLocalizedText();
            bool avoidable = investigation.HasValue(_configuration.IncidentInvestigation_Avoidable) ? investigation.GetProperty(_configuration.IncidentInvestigation_Avoidable).GetValue<bool>() : false;
            string abuseOrNeglect = investigation.GetProperty(_configuration.IncidentInvestigation_AbuseOrNeglect).GetValueAsLocalizedText();
            string suspectedCause = investigation.GetProperty(_configuration.IncidentInvestigation_SuspectedCause).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncInvAdditionalInformation";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@ChangeMedicalCondition", changeMedicalCondition);
            storedProc.storedProcParams.Add("@Assessment", assessment);
            storedProc.storedProcParams.Add("@ServicePlan", servicePlan);
            storedProc.storedProcParams.Add("@Physician", physician);
            storedProc.storedProcParams.Add("@SimilarIncidents", similarIncidents);
            storedProc.storedProcParams.Add("@Reported", reported);
            storedProc.storedProcParams.Add("@Avoidable", avoidable);
            storedProc.storedProcParams.Add("@AbuseOrNeglect", abuseOrNeglect);
            storedProc.storedProcParams.Add("@SuspectedCause", suspectedCause);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
        public void ExportIncidentInvestigationDetails(ObjVerEx investigation)
        {
            string investigationDoneBy = "";// = investigation.GetProperty(_configuration.IncidentInvestigation_InvestigationDoneBy).GetValueAsLocalizedText();
            string name = investigation.GetProperty(_configuration.IncidentInvestigation_Name).GetValueAsLocalizedText();

            string date = investigation.GetProperty(_configuration.IncidentInvestigation_Date).GetValueAsLocalizedText();
            string time = investigation.GetProperty(_configuration.IncidentInvestigation_Time).GetValueAsLocalizedText();
            DateTime dateOfInvestigation = DateTime.Parse($"{date} {time}");

            string locationOfIncident = investigation.GetProperty(_configuration.IncidentInvestigation_LocationOfIncident).GetValueAsLocalizedText();
            bool interviewConducted = investigation.HasValue(_configuration.IncidentInvestigation_InterviewConducted) ? investigation.GetProperty(_configuration.IncidentInvestigation_InterviewConducted).GetValue<bool>() : false;
            string interviewee = investigation.GetProperty(_configuration.IncidentInvestigation_Interviewee).GetValueAsLocalizedText();
            string comments = investigation.GetProperty(_configuration.IncidentInvestigation_CommentsNotes).GetValueAsLocalizedText();

            StoredProc storedProc = new StoredProc();
            storedProc.procedureName = "sp_ExportIncidentInvestigation";

            storedProc.storedProcParams = new Dictionary<string, object>();
            storedProc.storedProcParams.Add("@IncInvId", investigation.ID);
            storedProc.storedProcParams.Add("@IncInvDet", name);
            storedProc.storedProcParams.Add("@InvestigationDoneBy", investigationDoneBy);
            storedProc.storedProcParams.Add("@DateOfInvestigation", dateOfInvestigation);
            storedProc.storedProcParams.Add("@LocationOfIncident", locationOfIncident);
            storedProc.storedProcParams.Add("@InterviewConducted", interviewConducted);
            storedProc.storedProcParams.Add("@Interviewee", interviewee);
            storedProc.storedProcParams.Add("@Comments", comments);

            _databaseConnector.ExecuteStoredProc(storedProc);
        }
    }
}
