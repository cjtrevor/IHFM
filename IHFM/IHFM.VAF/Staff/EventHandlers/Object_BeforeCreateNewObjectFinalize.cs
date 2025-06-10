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
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.Staff")]
        public void AutoPopulateStaffPropertiesForMobile(EventHandlerEnvironment env)
        {
            StaffPropertyService staffPropertyService = new StaffPropertyService(env.Vault, Configuration);
            int createdById = env.ObjVerEx.Properties.SearchForProperty((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCreatedBy).TypedValue.GetLookupID();

            ObjVerEx staff = staffPropertyService.GetStaffObjVerExForUserId(createdById);
            ObjVerEx site = staffPropertyService.GetSiteObjectFromStaff(staff);

            var siteCareManagers = site.GetLookupsFromProperty(Configuration.SiteCareManagers);
            var siteAdministrators = site.GetLookupsFromProperty(Configuration.SiteAdministrators);
            var siteTeamLeaders = site.GetLookupsFromProperty(Configuration.SiteTeamLeaders);

            foreach (var scmItem in siteCareManagers)
            {
                env.ObjVerEx.AddLookup(Configuration.SiteCareManagers, scmItem.ToObjID().ID);
            }

            foreach (var saItem in siteAdministrators)
            {
                env.ObjVerEx.AddLookup(Configuration.SiteAdministrators, saItem.ToObjID().ID);
            }

            foreach (var stlItem in siteTeamLeaders)
            {
                env.ObjVerEx.AddLookup(Configuration.SiteTeamLeaders, stlItem.ToObjID().ID);
            }
        }
    }
}
