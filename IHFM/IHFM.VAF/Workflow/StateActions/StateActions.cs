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
            int siteId = res.GetLookupID(Configuration.BaseSiteID);

            env.ObjVerEx.SetProperty(Configuration.VAFSite, MFDataType.MFDatatypeLookup, siteId);
            env.ObjVerEx.SaveProperties();
        }
    }
}
