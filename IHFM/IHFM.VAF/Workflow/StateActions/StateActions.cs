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

        [StateAction("WFS.RoomLevyMaintenance.YearlyIncrease")]
        public void SetRoomLevyYearlyIncrease(StateEnvironment env)
        {
            SiteSearchService serv = new SiteSearchService(env.Vault, Configuration);

            int siteId = env.ObjVerEx.GetLookupID(Configuration.Site_BaseSiteDropdown);
            ObjVerEx siteConfig = serv.GetSiteConfig(siteId);

            double increasePerc = siteConfig.GetProperty(Configuration.SiteConfig_LevyIncrPerc).GetValue<double>();
            string currentValueText = env.ObjVerEx.GetProperty(Configuration.Room_Tariff).GetValueAsLocalizedText();

            double currentValue;

            if(!Double.TryParse(currentValueText, out currentValue))
            {
                SysUtils.ReportErrorMessageToEventLog("Error updating room levy via workflow", new Exception($"{currentValueText} is not a valid tariff for room: {env.ObjVerEx.Title}"));
                return;
            }

            double newValue = currentValue * (increasePerc + 100) / 100;

            int roundedValue = (int)(newValue + 0.5);

            ValueListItems valueListItems = env.Vault.ValueListItemOperations.GetValueListItems(Configuration.Room_TariffValueList.ID);

            ValueListItem item = null;

            foreach (ValueListItem valueItem in valueListItems)
            {
                if(valueItem.Name == roundedValue.ToString())
                {
                    item = valueItem;
                    break;
                }
            }

            if(item == null)
            {
                //does not exist
                ValueListItem tempItem = new ValueListItem();
                tempItem.Name = roundedValue.ToString();
                tempItem.ValueListID = Configuration.Room_TariffValueList.ID;

                item = env.Vault.ValueListItemOperations.AddValueListItem(Configuration.Room_TariffValueList.ID, tempItem);
            }

            env.ObjVerEx.SaveProperty(Configuration.Room_Tariff, MFDataType.MFDatatypeLookup, item.ID);
            env.ObjVerEx.RemoveProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefWorkflow);
            env.ObjVerEx.RemoveProperty(MFBuiltInPropertyDef.MFBuiltInPropertyDefState);
            env.ObjVerEx.SaveProperties();

            RoomPropertyService service = new RoomPropertyService(Configuration);
            service.UpdateRoomResidentTariff(env.ObjVerEx, item.ID, env.Vault);
        }

        
    }
}
