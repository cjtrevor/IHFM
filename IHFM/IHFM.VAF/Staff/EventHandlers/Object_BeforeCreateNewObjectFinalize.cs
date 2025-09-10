using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, ObjectType = "MFiles.Object.Staff")]
        public void SetSiteIdNewStaff(EventHandlerEnvironment env)
        {
            Lookup siteLookup = env.ObjVerEx.GetProperty(Configuration.Staff_Site).TypedValue.GetValueAsLookup();
            ObjVerEx site = new ObjVerEx(env.Vault, siteLookup);

            int siteId = site.GetLookupID(Configuration.Staff_SiteId);

            env.ObjVerEx.SetProperty(Configuration.Staff_SiteId, MFDataType.MFDatatypeLookup, siteId);
        }
    }
}
