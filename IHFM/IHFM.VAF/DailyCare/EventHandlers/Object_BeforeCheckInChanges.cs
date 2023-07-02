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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.DailyCare")]
        public void BeforeChcekInDailyCare(EventHandlerEnvironment env)
        {
            LogCompletedCare(env.ObjVerEx);
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
