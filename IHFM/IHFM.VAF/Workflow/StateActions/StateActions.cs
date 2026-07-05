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

        [StateAction("WFS.Medsgivenauto.Populatemedsonscript")]
        public void SetMedsGivenAutoMedsOnScript(StateEnvironment env)
        {
            List<int> addedValues = new List<int>();

            string pipes = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_MDDValues);

            foreach (string val in pipes.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (addedValues.Contains(Int32.Parse(val)))
                    continue;

                Lookup objLookup = new Lookup() { Item = Int32.Parse(val) }; //GetLookupFromVal(env.Vault,Int32.Parse(val));
                //Lookup objLookup = GetLookupFromVal11(env.Vault, Int32.Parse(val));

                //if (objLookup == null)
                //{
                //    continue;
                //}

                env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, objLookup.GetAsObjVer());
                addedValues.Add(Int32.Parse(val));
            }

            string timeslot = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_Timeslot);

            ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration, env.Vault);
            env.ObjVerEx.SetProperty(Configuration.AutoShift, MFDataType.MFDatatypeText, shiftCalculationService.CalculateAutoShiftNumberBySiteIdByResident(env.ObjVerEx, timeslot));

            env.ObjVerEx.SaveProperties();
        }

        private Lookup GetLookupFromVal11(Vault vault, int val)
        {
            MFSearchBuilder search = new MFSearchBuilder(vault);
            search.ObjType(Configuration.MDDAuto_MDDObjectId.ID);

            SearchCondition byId = new SearchCondition();
            byId.Expression.SetStatusValueExpression(MFStatusType.MFStatusTypeObjectID);
            byId.ConditionType = MFConditionType.MFConditionTypeEqual;
            byId.TypedValue.SetValue(MFDataType.MFDatatypeInteger, val);
            search.Conditions.Add(-1, byId);

            ObjVerEx found = search.FindOneEx();
            return found?.ToLookup();
        }

    }
}
