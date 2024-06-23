using MFilesAPI;
using MFiles.VAF.Common;
using System;
using SSRS_Reporting.Services;
using System.IO;
using IHFM.EmailService;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        private byte[] GetReport(ObjVerEx obj, bool includeDate = true)
        {
            MaintenanceRequestReports reports = new MaintenanceRequestReports();

            string objectId = obj.ObjVer.ID.ToString();
            string resident = obj.GetProperty(Configuration.MaintReq_Resident).GetValueAsLocalizedText();
            string staff = obj.GetProperty(Configuration.MaintReq_Staff).GetValueAsLocalizedText();
            string jobAssignedTo = obj.GetProperty(Configuration.MaintReq_JobAssignedTo).GetValueAsLocalizedText();
            string jobToBeDone = obj.GetProperty(Configuration.MaintReq_JobToBeDone).GetValueAsLocalizedText();
            string jobDate = includeDate ? DateTime.Now.ToShortDateString() : "";
            string timeStarted = obj.GetProperty(Configuration.MaintReq_TimeStarted).GetValueAsLocalizedText();
            string timeFinished = obj.GetProperty(Configuration.MaintReq_TimeFinished).GetValueAsLocalizedText();
            string comments = obj.GetProperty(Configuration.MaintReq_CommentsNotes).GetValueAsLocalizedText();

            return reports.GetMaintenanceRequestReport(objectId, resident, staff, jobAssignedTo, jobToBeDone, jobDate, timeStarted, timeFinished, comments);
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Priority = -1, Class = "MFiles.Class.MaintenanceRequest")]
        public void BeforeCreateNewMaintenanceRequestFinalised(EventHandlerEnvironment env)
        {
            EmailConfig config = new EmailConfig
            {
                SMTP = Configuration.Email_SMTP,
                FriendlyName = Configuration.Email_FriendlyAddress,
                FromAddress = Configuration.Email_FromAddress,
                Password = Configuration.Email_Password,
                Username = Configuration.Email_Username
            };

            EmailServiceLegacy mailService = new EmailServiceLegacy(config);

            //IF contractor send mail
            Lookup staffLookup = env.ObjVerEx.GetProperty(Configuration.MaintReq_JobAssignedTo).TypedValue.GetValueAsLookup();
            ObjVerEx staffObj = new ObjVerEx(env.Vault, staffLookup);

            if (staffObj.GetLookupID(Configuration.MaintReq_Staff_EmploymentStatus) == Configuration.MaintReq_Staff_ContractorStatus.ID)
            {
                string email = staffObj.GetPropertyText(Configuration.MaintReq_Staff_Email);
                if (!string.IsNullOrEmpty(email))
                {
                    string site = env.ObjVerEx.GetProperty(Configuration.BaseSite).GetValueAsLocalizedText();
                    string subject = Configuration.MRContractor_SubjectLine.Replace("|Site|", site);
                    string body = Configuration.MRContractor_BodyLine;

                    string objectId = env.ObjVer.ID.ToString();
                    byte[] rep = GetReport(env.ObjVerEx, false);

                    mailService.SendDefaultEmailWithAttachment(rep, $"{objectId}.pdf", email, subject, body);
                }
            }

        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.MaintenanceRequest")]
        public void BeforeNewMaintenanceRequestCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            if(!env.ObjVerEx.HasValue(Configuration.MaintReq_Resident) || 
                (env.ObjVerEx.HasValue(Configuration.MaintReq_PrintPDF) && env.ObjVerEx.GetProperty(Configuration.MaintReq_PrintPDF).GetValue<bool>() == false))
            {
                return;
            }

            bool statusChanged = false;

            ObjVerChanges changes = new ObjVerChanges(env.ObjVerEx);

            foreach (PropertyValueChange change in changes.Changed)
            {
                if(change.PropertyDef == Configuration.MaintReq_JobStatus.ID)
                {
                    statusChanged = true;
                }
            }

            if (statusChanged && env.ObjVerEx.GetProperty(Configuration.MaintReq_JobStatus).GetValueAsLocalizedText().Contains("Completed"))
            {
                string objectId = env.ObjVer.ID.ToString();
                byte[] rep = GetReport(env.ObjVerEx);

                File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);

                env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
                env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");

                File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
            }
        }
    }
}
