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

            //ExportScriptManagement(env, siteId);
        }
        private void ExportScriptManagement(StateEnvironment env, int siteId)
        {
            ScriptControlExportService service = new ScriptControlExportService(env.Vault, Configuration);
            service.ExportScriptControl(env.ObjVerEx, siteId);
        }

        [StateAction("WFS.Medsgivenauto.Populatemedsonscript")]
        public void SetMedsGivenAutoMedsOnScript(StateEnvironment env)
        {
            List<int> addedValues = new List<int>();

            string pipes = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_MDDValues);

            foreach (string val in pipes.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (addedValues.Contains(Int32.Parse(val)))
                    continue;

                Lookup objLookup = new Lookup() { Item = Int32.Parse(val) };//GetLookupFromVal(env.Vault,Int32.Parse(val));

                env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, objLookup.GetAsObjVer());
                addedValues.Add(Int32.Parse(val));
            }

            string timeslot = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_Timeslot);

            ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration, env.Vault);
            env.ObjVerEx.SetProperty(Configuration.AutoShift, MFDataType.MFDatatypeText, shiftCalculationService.CalculateAutoShiftNumberBySiteIdByResident(env.ObjVerEx, timeslot));

            env.ObjVerEx.SaveProperties();
        }

        [StateAction("WFS.MaintenanceRequest.Adjuststocklevels")]
        public void UpdateSiteStockFromMaintenanceRequest(StateEnvironment env)
        {
            int siteID = env.ObjVerEx.GetLookupID(Configuration.Site_SiteIdBySite);

            SiteStockUpdateService service = new SiteStockUpdateService(env.Vault, Configuration);
            service.CreateNewStockIssue(siteID, env.ObjVerEx, true);
        }

    }
}
