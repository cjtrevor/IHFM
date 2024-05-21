using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [StateAction("MFiles.WorkflowState.ScriptVerifiedCorrect")]
        public void SetItemSiteOnScriptVerifiedCorrect(StateEnvironment env)
        {
            var residentLookup = env.ObjVerEx.GetProperty(Configuration.ResidentLookup).TypedValue.GetValueAsLookup();

            ObjVerEx res = new ObjVerEx(env.Vault, residentLookup);
            int vafSiteId = res.GetLookupID(Configuration.BaseSiteID);
            int siteId = res.GetLookupID(Configuration.BaseSite);

            env.ObjVerEx.SetProperty(Configuration.VAFSite, MFDataType.MFDatatypeLookup, vafSiteId);
            env.ObjVerEx.SaveProperties();

            ExportScriptManagement(env, siteId);
        }
        private void ExportScriptManagement(StateEnvironment env, int siteId)
        {
            ScriptControlExportService service = new ScriptControlExportService(env.Vault, Configuration);
            service.ExportScriptControl(env.ObjVerEx, siteId);
        }

        [StateAction("WFS.Medsgivenauto.Populatemedsonscript")]
        public void SetMedsGivenAutoMedsOnScript(StateEnvironment env)
        {
            Lookups medsLookups = env.ObjVerEx.GetLookups(Configuration.MDDAuto_AutoMedsOnScript);

            foreach (Lookup lookup in medsLookups)
            {
                env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, lookup.GetAsObjVer());
            }

            env.ObjVerEx.SaveProperties();
        }

    }
}
