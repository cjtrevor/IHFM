using System;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.IncidentInvestigation")]
        public void AfterCreateNewIncidentInvestigation(EventHandlerEnvironment env)
        {
            SetInvestigationDoneOnProgressNote(env.ObjVerEx, env.Vault);
            ExportIncidentInvestigation(env.ObjVerEx, env.Vault);
        }

        private void ExportIncidentInvestigation(ObjVerEx investigation, Vault vault)
        {
            IncidentInvestigationExportService exportService = new IncidentInvestigationExportService(vault, Configuration);
            exportService.ExportIncidentInvestigation(investigation);
        }

        public void SetInvestigationDoneOnProgressNote(ObjVerEx incident, Vault vault)
        {
            ObjVerEx note = new ObjVerEx(vault, incident.GetProperty(Configuration.IncidentReport_RegardingIncident).TypedValue.GetValueAsLookup());
            note.SaveProperty(Configuration.IncidentReport_InvestigationDone, MFDataType.MFDatatypeBoolean, true);
        }
    }
}