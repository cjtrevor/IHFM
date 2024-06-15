using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSRS_Reporting.Services;
using System.IO;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Priority = -1, Class = "MFiles.Class.MaintenanceRequest")]
        public void BeforeNewMaintenanceRequestCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            return;

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
                MaintenanceRequestReports reports = new MaintenanceRequestReports();

                string objectId = env.ObjVer.ID.ToString();
                string resident = env.ObjVerEx.GetProperty(Configuration.MaintReq_Resident).GetValueAsLocalizedText();
                string staff = env.ObjVerEx.GetProperty(Configuration.MaintReq_Staff).GetValueAsLocalizedText();
                string jobAssignedTo = env.ObjVerEx.GetProperty(Configuration.MaintReq_JobAssignedTo).GetValueAsLocalizedText();
                string jobToBeDone = env.ObjVerEx.GetProperty(Configuration.MaintReq_JobToBeDone).GetValueAsLocalizedText();
                string jobDate = DateTime.Now.ToShortDateString();
                string timeStarted = env.ObjVerEx.GetProperty(Configuration.MaintReq_TimeStarted).GetValueAsLocalizedText();
                string timeFinished = env.ObjVerEx.GetProperty(Configuration.MaintReq_TimeFinished).GetValueAsLocalizedText();
                string comments = env.ObjVerEx.GetProperty(Configuration.MaintReq_CommentsNotes).GetValueAsLocalizedText();

                byte[] rep = reports.GetMaintenanceRequestReport(objectId, resident, staff, jobAssignedTo, jobToBeDone, jobDate, timeStarted, timeFinished, comments);

                File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);

                env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
                env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"MR{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");

                File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
            }
        }
    }
}
