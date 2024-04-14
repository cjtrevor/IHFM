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
            Lookup resLookup = env.ObjVerEx.GetProperty(Configuration.MDDAuto_Resident).TypedValue.GetValueAsLookup();
            ObjVerEx resident = new ObjVerEx(env.Vault, resLookup);

            env.ObjVerEx.SetProperty(Configuration.MDDAuto_Allergies, MFDataType.MFDatatypeText, resident.GetPropertyText(Configuration.MDDAuto_Allergies));
        }
    }
}
