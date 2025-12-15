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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.PanicButtonTest")]
        public void BeforeNewPanicButtonTestCheckinChangesFinalize(EventHandlerEnvironment env)
        {
            if (env.ObjVerEx.HasValue(Configuration.PanicButtonTest_PrintPDF) && env.ObjVerEx.GetProperty(Configuration.PanicButtonTest_PrintPDF).GetValue<bool>() == false)
                return;

            string objectId = env.ObjVer.ID.ToString();

            var parameterJsonData = new
            {
                ObjectId = objectId,
                Site = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_Site),
                Date = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_Date),
                PbRoom = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_PbRoom),
                CurrentRoom = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_CurrentRoom),
                MaintenanceLocation = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_MaintenanceLocation),
                PanicButtonWorking = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_PanicButtonWorking),
                CommentsNotes = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_CommentsNotes),
                ReportToMaintenanceManager = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_ReportToMaintenanceManager),
                CreatedBy = GetPropertyValueAsText(env.ObjVerEx, Configuration.PanicButtonTest_CreatedBy)
            };

            var serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(parameterJsonData);

            PanicButtonReports reports = new PanicButtonReports();
            byte[] rep = reports.GetReport("PanicButtonPdf", serializedJson);

            File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.pdf", rep);
            env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
            env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"PBT{objectId}-{env.ObjVerEx.Version}", "pdf", $"C:\\SSRS Temp Output\\{objectId}.pdf");
            File.Delete($"C:\\SSRS Temp Output\\{objectId}.pdf");
        }
        
    }
}
