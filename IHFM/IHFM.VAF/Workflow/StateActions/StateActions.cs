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
        public readonly int[] PropertiesToRemove =
        {
            (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefWorkflow,
            (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefState
        };

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

        [StateAction("WFS.Medslistmaintenance.Createtradecopy")]
        public void CreateTradeCopyMedsListItem(StateEnvironment env)
        {
            var newObjectPropertyValues = this.GetNewObjectPropertyValues(env.PropertyValues);

            string tradeName = newObjectPropertyValues.GetProperty(Configuration.MedsGiven_TradeName.ID).GetValueAsLocalizedText();
            newObjectPropertyValues.SetProperty(Configuration.MedsGiven_GenericName.ID, MFDataType.MFDatatypeText, tradeName);

            env.Vault.ObjectOperations.CreateNewObjectExQuick(
                env.ObjVer.Type,
                newObjectPropertyValues,
                null,
                false,
                CheckIn: true,
                AccessControlList: null); ;
        }

        private PropertyValues GetNewObjectPropertyValues(PropertyValues cloneFrom)
        {
          // Sanity.
            if (null == cloneFrom)
                throw new ArgumentNullException(nameof(cloneFrom));
            // Get a basic copy.
            var propertyValues = cloneFrom.Clone();
            // Remove the properties we don't want.
            foreach (var propertyId in this.PropertiesToRemove)
            {
                // If the property is not in the collection then skip.
                int index = propertyValues.IndexOf(propertyId);
                if (-1 == index)
                    continue;
                // Remove it.
                propertyValues.Remove(index);
            }
            // Return.
            return propertyValues;
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

                Lookup objLookup = new Lookup() { Item = Int32.Parse(val) };

                env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, objLookup.GetAsObjVer());
                addedValues.Add(Int32.Parse(val));
            }

            string timeslot = env.ObjVerEx.GetPropertyText(Configuration.MDDAuto_Timeslot);

            ShiftCalculationService shiftCalculationService = new ShiftCalculationService(Configuration, env.Vault);
            env.ObjVerEx.SetProperty(Configuration.AutoShift, MFDataType.MFDatatypeText, shiftCalculationService.CalculateAutoShiftNumberBySiteIdByResident(env.ObjVerEx, timeslot));

            env.ObjVerEx.SaveProperties();
        }
    }
}
