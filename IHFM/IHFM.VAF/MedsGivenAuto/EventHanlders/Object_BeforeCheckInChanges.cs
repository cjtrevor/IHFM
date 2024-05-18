using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.MedsGivenAuto", Priority = 100)]
        public void BeforeCheckInChangesMedsGivenAuto(EventHandlerEnvironment env)
        {
            LogMissedMedsGivenAuto(env.ObjVerEx);
        }

        private void LogMissedMedsGivenAuto(ObjVerEx medsGiven)
        {
            ObjVerChanges changes = new ObjVerChanges(medsGiven);
            foreach (PropertyValueChange changed in changes.Changed)
            {
                if (changed.PropertyDef == Configuration.MedsOnScript.ID)
                {
                    if (changed.OldValue == null || changed.NewValue == null)
                    {
                        continue;
                    }

                    Lookups oldMeds = changed.OldValue.TypedValue.GetValueAsLookups();
                    Lookups newMeds = changed.NewValue.TypedValue.GetValueAsLookups();

                    foreach (Lookup old in oldMeds)
                    {
                        if (newMeds.GetLookupIndexByItem(old.Item) == -1)
                        {
                            medsGiven.AddLookup(Configuration.ScriptControl_MissedMeds, old.GetAsObjVer());
                        }
                    }

                    medsGiven.SaveProperties();
                }
            }
        }
    }
}
