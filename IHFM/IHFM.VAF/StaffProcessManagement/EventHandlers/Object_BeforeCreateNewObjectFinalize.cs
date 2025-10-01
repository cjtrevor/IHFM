using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using Newtonsoft.Json;
using SSRS_Reporting.Services;
using System;
using System.IO;
using System.Linq;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.PolicyAcknowledgement")]
        public void BeforePolicyAcknowledgementCheckInChangesFizalize(EventHandlerEnvironment env)
        {
            string reportFileOutputDir = "C:\\ReportingConfigurator\\SSRS Temp Output\\";
            string objectId = env.ObjVer.ID.ToString();
            var staffObjects = env.ObjVerEx.GetProperty(Configuration.StaffProcessManagement_Staffs).TypedValue.GetValueAsLookups().ToObjVerExs(env.Vault);
            var policyDocumentsLookups = env.ObjVerEx.GetProperty(Configuration.StaffProcessManagement_PolicyDocuments).TypedValue.GetValueAsLookups();

            var policyDocumentsNames = string.Empty;

            foreach (Lookup policy in policyDocumentsLookups)
                policyDocumentsNames += $"{policy.DisplayValue}{System.Environment.NewLine}";

            ProofOfSeenReportService reportService = new ProofOfSeenReportService();

            foreach (var staff in staffObjects)
            {
                var staffReportJsonData = JsonConvert.SerializeObject(new
                {
                    Name = staff.Title,
                    GenderTitle = staff.GetProperty(Configuration.Staff_GenderTitle).GetValueAsLocalizedText(),
                    Site = staff.GetProperty(Configuration.Staff_Site).GetValueAsLocalizedText(),
                    PolicyDocuments = policyDocumentsNames,
                    ObjectName = env.ObjVerEx.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText(),
                    ObjectId = objectId,
                    Date = env.ObjVerEx.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText()
                });

                var fileNameStaffIdentifier = $"{staff.GetPropertyText(Configuration.Staff_Name)} {staff.GetPropertyText(Configuration.Staff_Surname)}({staff.ObjID.ID})" ;

                var staffReportFile = reportService.GetReport("PolicyCompliance", staffReportJsonData);

                File.WriteAllBytes($"{reportFileOutputDir}{objectId}_{fileNameStaffIdentifier}.pdf", staffReportFile);
                env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
                env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"PoS{objectId}_{fileNameStaffIdentifier}-{env.ObjVerEx.Version}", "pdf", $"{reportFileOutputDir}{objectId}_{fileNameStaffIdentifier}.pdf");
                File.Delete($"{reportFileOutputDir}{objectId}_{fileNameStaffIdentifier}.pdf");
            }

            if (staffObjects.Count > 1)
            {
                var allStaffReportJsonData = JsonConvert.SerializeObject(new
                {
                    Staff = staffObjects.Select(x =>
                    {
                        var genderTitle = x.GetProperty(Configuration.Staff_GenderTitle).GetValueAsLocalizedText();
                        var site = x.GetProperty(Configuration.Staff_Site).GetValueAsLocalizedText();
                        return new
                        {
                            Name = x.Title,
                            GenderTitle = genderTitle,
                            Site = site
                        };
                    }
                    ),
                    PolicyDocuments = policyDocumentsNames,
                    ObjectName = env.ObjVerEx.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle).GetValueAsLocalizedText(),
                    ObjectId = objectId,
                    Date = env.ObjVerEx.GetProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated).GetValueAsLocalizedText()
                });

                var allStaffReportFile = reportService.GetReport("PolicyCompliances", allStaffReportJsonData);

                File.WriteAllBytes($"{reportFileOutputDir}{objectId}.pdf", allStaffReportFile);
                env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
                env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"PoS{objectId}-{env.ObjVerEx.Version}", "pdf", $"{reportFileOutputDir}{objectId}.pdf");
                File.Delete($"{reportFileOutputDir}{objectId}.pdf");
            }
        }
    }
}
