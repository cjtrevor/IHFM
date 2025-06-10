using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.SistersInstruction")]
        public void AutoPopulateSistersInstructionsPropertiesForMobile(EventHandlerEnvironment env)
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
