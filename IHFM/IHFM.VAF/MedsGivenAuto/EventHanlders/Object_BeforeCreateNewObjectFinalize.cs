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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.MedsGivenAuto")]
        public void BeforeCreateNewMDDAutoFinalize(EventHandlerEnvironment env)
        {
            Lookups medsLookups = env.ObjVerEx.GetLookups(Configuration.MDDAuto_AutoMedsOnScript);

            foreach(Lookup lookup in medsLookups)
            {
                env.ObjVerEx.AddLookup(Configuration.MDDAuto_MedsOnScript, lookup.GetAsObjVer());
            }
        }
    }
}
