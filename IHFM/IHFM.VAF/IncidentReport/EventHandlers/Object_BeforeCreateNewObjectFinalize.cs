using System;
using MFiles.VAF.Common;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.IncidentInvestigation")]
        public void BeforeCreateNewIncidentInvestigation(EventHandlerEnvironment env)
        {
            SetInvestigationDoneOnProgressNote(env.ObjVerEx, env.Vault);
        }

        public void SetInvestigationDoneOnProgressNote(ObjVerEx incident, Vault vault)
        {
            ObjVerEx note = new ObjVerEx(vault, incident.GetProperty(Configuration.IncidentReport_RegardingIncident).TypedValue.GetValueAsLookup());
            note.SaveProperty(Configuration.IncidentReport_InvestigationDone, MFDataType.MFDatatypeBoolean, true);
        }
    }
}