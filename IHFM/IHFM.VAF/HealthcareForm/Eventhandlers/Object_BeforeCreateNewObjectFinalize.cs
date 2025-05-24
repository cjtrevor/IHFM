using MFilesAPI;
using MFiles.VAF.Common;
using System;
using SSRS_Reporting.Services;
using System.IO;
using IHFM.EmailService;
using System.Collections.Generic;
using System.Security.Policy;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, ObjectType = "OT.IncidentReport")]
        public void BeforeNewHealthcareFormCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            return;
            //List<int> pdfClasses = new List<int>
            //{
            //    Configuration.ConsentToMedia_Class.ID,
            //    Configuration.RestraintRecord_Class.ID,
            //    Configuration.ExclusionOfLI_Class.ID
            //};

            //if (!pdfClasses.Contains(env.ObjVerEx.Class))
            //{
            //    return;
            //}

            //byte[] rep;

            //switch (env.ObjVerEx.Class)
            //{
            //    case var id when id == Configuration.ConsentToMedia_Class.ID:
            //        rep = GetReport_ConsentToMedia(env.ObjVerEx);
            //        break;
            //    case var id when id == Configuration.RestraintRecord_Class.ID:
            //        //rep = GetReport_RestraintRecord(env.ObjVerEx);
            //        break;
            //    case var id when id == Configuration.ExclusionOfLI_Class.ID:
            //        //rep = GetReport_ExclusionOfLI(env.ObjVerEx);
            //        break;
            //    default:
            //        return;
            //}
           
            //Lookup resLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            //ObjVerEx resident = new ObjVerEx(env.Vault, resLookup);

            //string objectId = env.ObjVer.ID.ToString();
            //File.WriteAllBytes($"C:\\SSRS Temp Output\\01PranMan_{objectId}.pdf", rep);

            //env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            //env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");

            //File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");


            //throw new Exception("try again bruh");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.ConsentToVideopictureRelease")]
        public void BeforeNewConsentToMediaCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVer.ID.ToString();

            var parameterJsonData = new
            {
                Site = env.ObjVerEx.GetProperty(Configuration.BaseSite).GetValueAsLocalizedText(),
                Resident = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).GetValueAsLocalizedText(),
                Date = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_Date).GetValueAsLocalizedText(),
                ObjectId = objectId,

                Relationship = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_Relationship).GetValueAsLocalizedText(),
                CompletedBy = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_CreatedBy).GetValueAsLocalizedText(),
                FamilyPermissionDate = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_FamilyPermissionDate).GetValueAsLocalizedText()
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            HealthcareFormReports reports = new HealthcareFormReports();
            byte[] rep = reports.GetReport("Consent_to_media", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.ExclusionOfLiabilityAndIndemnity")]
        public void BeforeNewExclusionOfLICheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVer.ID.ToString();

            var parameterJsonData = new
            {
                Site = env.ObjVerEx.GetProperty(Configuration.BaseSite).GetValueAsLocalizedText(),
                Resident = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).GetValueAsLocalizedText(),
                Date = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_Date).GetValueAsLocalizedText(),
                ObjectId = objectId,

                NextOfKin = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_NextOfKin).GetValueAsLocalizedText(),
                NextOfKinIdNumber = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_NextOfKinIdNumber).GetValueAsLocalizedText(),
                NextOfKinCellNumber = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_NextOfKinCellNumber).GetValueAsLocalizedText(),
                Relationship = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_Relationship).GetValueAsLocalizedText(),
                CareManagerAcknowledgement = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_CareManagerAcknowledgement).GetValueAsLocalizedText(),
                ProposedSolutionsAcknowledgement = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_ProposedSolutionsAcknowledgement).GetValueAsLocalizedText(),
                FamilyPermissionDate = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_FamilyPermissionDate).GetValueAsLocalizedText()
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            HealthcareFormReports reports = new HealthcareFormReports();
            byte[] rep = reports.GetReport("Exclusion_of_liability", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.RestraintRecord")]
        public void BeforeNewRestraintRecordCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVer.ID.ToString();

            var parameterJsonData = new
            {
                Site = env.ObjVerEx.GetProperty(Configuration.BaseSite).GetValueAsLocalizedText(),
                Resident = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).GetValueAsLocalizedText(),
                Date = env.ObjVerEx.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText(),
                ObjectId = objectId,

                RestraintType = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_RestraintType).GetValueAsLocalizedText(),
                ReasonForRestraint = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_ReasonForResident).GetValueAsLocalizedText(),
                FamilyMemberPermission = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_FamilyPermissionDate).GetValueAsLocalizedText(),
                Relationship = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_Relationship).GetValueAsLocalizedText(),
                FamilyPermissionDate = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_FamilyPermissionDate).GetValueAsLocalizedText(),
                DoctorPermissionDate = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_DoctorPermissionDate).GetValueAsLocalizedText(),
                RestraintsPermitted = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_RestraintsPermitted).GetValueAsLocalizedText(),
                RestrictionPeriodsAllowed = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_RestrictionPeriodsAllowed).GetValueAsLocalizedText(),
                RiskFactors = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_RiskFactors).GetValueAsLocalizedText(),
                Notes = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_CommentsNotes).GetValueAsLocalizedText(),
                NextOfKin = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_NextOfKin).GetValueAsLocalizedText(),
                NextOfKinIdNumber = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_NextOfKinIdNumber).GetValueAsLocalizedText(),
                NextOfKinCellNumber = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_NextOfKinCellNumber).GetValueAsLocalizedText()
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            HealthcareFormReports reports = new HealthcareFormReports();
            byte[] rep = reports.GetReport("Exclusion_of_liability", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }

    }
}
