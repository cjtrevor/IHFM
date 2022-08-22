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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.MedsGiven", Priority = 100)]
        public void BeforeCheckInChangesMedsGiven(EventHandlerEnvironment env)
        {
            if(DevelopmentUtility.IsDevMode(env.ObjVerEx, Configuration))
                LogMissedMeds(env.ObjVerEx);
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChanges, Class = "MFiles.Class.PrnMedsGiven", Priority = 100)]
        public void BeforeCheckInChangesPrnMedsGiven(EventHandlerEnvironment env)
        {
            if (DevelopmentUtility.IsDevMode(env.ObjVerEx, Configuration))
                LogMissedMeds(env.ObjVerEx);
        }

        public void LogMissedMeds(ObjVerEx medsGiven)
        {
            ObjVerChanges changes = new ObjVerChanges(medsGiven);
            foreach(PropertyValueChange changed in changes.Changed)
            {
               if(changed.PropertyDef == Configuration.MedsOnScript.ID)
               {
                    Lookups oldMeds = changed.OldValue.TypedValue.GetValueAsLookups();
                    Lookups newMeds = changed.NewValue.TypedValue.GetValueAsLookups();

                    foreach(Lookup old in oldMeds)
                    {
                        if(newMeds.GetLookupIndexByItem(old.Item) == -1)
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
