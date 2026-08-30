using MFiles.VAF.Common;
using MFiles.VAF.Extensions;
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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.DailyCare")]
        public void BeforeChcekInDailyCare(EventHandlerEnvironment env)
        {
            LogCompletedCare(env.ObjVerEx);
        }


        //TEMPORARY
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.CombinedCareRecord")]
        public void BeforeChcekInCombinedCare(EventHandlerEnvironment env)
        {
            var tempClassUpdate = env.ObjVerEx.GetPropertyAsBoolean(Configuration.CombinedCare_TemporaryClassUpdate) ?? false;
            var dateCreated = env.ObjVerEx.GetPropertyAsDateTime((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCreated);

            if (dateCreated < DateTime.Parse("2026-08-05") && !tempClassUpdate)
            {
                if (env.ObjVerEx.GetPropertyAsBoolean(Configuration.CombinedCare_IntakeAndOutput) ?? false)
                {
                    InputOutputService inputOutputService = new InputOutputService(env.Vault, Configuration);
                    inputOutputService.UpdateInputOutputForShift(env.ObjVerEx);
                }

                if (env.ObjVerEx.GetPropertyAsBoolean(Configuration.CombinedCare_IncontineceCare) ?? false)
                {
                    DailyCareService dailyCareService = new DailyCareService(env.Vault, Configuration);
                    dailyCareService.UpdateNappyStock(env.ObjVerEx);
                }

                env.ObjVerEx.SaveProperty(Configuration.CombinedCare_TemporaryClassUpdate, MFDataType.MFDatatypeBoolean, true);
            }
        }

        private void LogCompletedCare(ObjVerEx dailyCare)
        {
            ObjVerChanges changes = new ObjVerChanges(dailyCare);
            foreach (PropertyValueChange changed in changes.Changed)
            {
                if (changed.PropertyDef == Configuration.TBCS_TimeBasedCareScheduleDropdown.ID)
                {
                    if (changed.OldValue == null || changed.NewValue == null)
                    {
                        continue;
                    }

                    Lookups oldTbcs = changed.OldValue.TypedValue.GetValueAsLookups();
                    Lookups newTbcs = changed.NewValue.TypedValue.GetValueAsLookups();

                    foreach (Lookup old in oldTbcs)
                    {
                        if (newTbcs.GetLookupIndexByItem(old.Item) == -1)
                        {
                            dailyCare.AddLookup(Configuration.TBCS_CompletedCare, old.GetAsObjVer());
                        }
                    }

                    dailyCare.SaveProperties();
                }
            }
        }
    }
}
