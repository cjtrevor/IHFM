using MFilesAPI;
using MFiles.VAF.Common;
using System;
using SSRS_Reporting.Services;
using System.IO;
using IHFM.EmailService;
using System.Collections.Generic;
using System.Security.Policy;
using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.ConsentToVideopictureRelease")]
        public void BeforeNewConsentToMediaCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVer.ID.ToString();

            var parameterJsonData = new
            {
                Site = GetPropertyValueAsText(env.ObjVerEx, Configuration.BaseSite),
                Resident = GetPropertyValueAsText(env.ObjVerEx, Configuration.ResidentLookup),
                Date = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_Date),
                ObjectId = objectId,

                Relationship = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_Relationship),
                OnBehalfOfResident = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_OnBehalfOfResident).GetValueAsLocalizedText(),
                CompletedBy = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_CreatedBy),
                FamilyPermissionDate = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_FamilyPermissionDate)
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            HealthcareFormReports reports = new HealthcareFormReports();
            byte[] rep = reports.GetReport("Consent_to_media_Sites", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"CTV{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.ExclusionOfLiabilityAndIndemnity")]
        public void BeforeNewExclusionOfLICheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVer.ID.ToString();

            var parameterJsonData = new
            {
                Site = GetPropertyValueAsText(env.ObjVerEx, Configuration.BaseSite),
                Resident = GetPropertyValueAsText(env.ObjVerEx, Configuration.ResidentLookup),
                Date = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_Date),
                ObjectId = objectId,

                NextOfKin = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_NextOfKin),
                NextOfKinIdNumber = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_NextOfKinIdNumber),
                NextOfKinCellNumber = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_NextOfKinCellNumber),
                Relationship = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_Relationship),
                CareManagerAcknowledgement = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_CareManagerAcknowledgement),
                ProposedSolutionsAcknowledgement = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_ProposedSolutionsAcknowledgement),
                FamilyPermissionDate = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_FamilyPermissionDate)
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            HealthcareFormReports reports = new HealthcareFormReports();
            byte[] rep = reports.GetReport("Exclusion_of_liability_Sites", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"ELI{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.RestraintRecord")]
        public void BeforeNewRestraintRecordCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            string objectId = env.ObjVer.ID.ToString();

            Lookup residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, residentLookup);

            Lookup lookupSite = resident.GetProperty(Configuration.BaseSite).TypedValue.GetValueAsLookup();

            var parameterJsonData = new
            {
                Site = lookupSite.DisplayValue,
                Resident = GetPropertyValueAsText(env.ObjVerEx, Configuration.ResidentLookup),
                Date = GetPropertyValueAsText(env.ObjVerEx, MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated),
                ObjectId = objectId,

                CompletedBy = env.ObjVerEx.GetProperty(Configuration.HealthcareForm_CreatedBy).GetValueAsLocalizedText(),
                RestraintType = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_RestraintType),
                ReasonForRestraint = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_ReasonForResident),
                FamilyMemberPermission = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_PermissionFromFamilyMember),
                Relationship = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_Relationship),
                FamilyPermissionDate = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_FamilyPermissionDate),
                DoctorPermissionDate = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_DoctorPermissionDate),
                RestraintsPermitted = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_RestraintsPermitted),
                RestrictionPeriodsAllowed = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_RestrictionPeriodsAllowed),
                RiskFactors = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_RiskFactors),
                Notes = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_CommentsNotes),
                NextOfKin = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_NextOfKin),
                NextOfKinIdNumber = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_NextOfKinIdNumber),
                NextOfKinCellNumber = GetPropertyValueAsText(env.ObjVerEx, Configuration.HealthcareForm_NextOfKinCellNumber)
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            HealthcareFormReports reports = new HealthcareFormReports();
            byte[] rep = reports.GetReport("Restraint_Sites", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"RR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }

        private string GetPropertyValueAsText(ObjVerEx objVerEx, MFIdentifier propertyDef)
        {
            string returnVal = string.Empty;

            if (objVerEx.TryGetProperty(propertyDef, out PropertyValue prop))
            {
                if (prop.Value.DataType == MFDataType.MFDatatypeMultiSelectLookup)
                {
                    foreach (Lookup item in prop.TypedValue.GetValueAsLookups())
                    {
                        returnVal += $"{item.DisplayValue}{System.Environment.NewLine}";
                    }
                }
                else
                {
                    returnVal = prop.GetValueAsLocalizedText();
                }
            }
            return returnVal;
        }
    }
}
