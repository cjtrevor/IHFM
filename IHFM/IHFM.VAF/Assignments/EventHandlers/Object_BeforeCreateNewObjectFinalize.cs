using System;
using System.Collections.Generic;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize)]
        public void SetAssignmentDropdowns(EventHandlerEnvironment env)
        {
            if (env.ObjVerEx.Class == 1180
                || env.ObjVerEx.Class == Configuration.Site_Class.ID
                || env.ObjVerEx.Class == Configuration.Staff.ID
                || env.ObjVerEx.Class == Configuration.MDDAuto_Class.ID)
                return;

            StaffPropertyService staffPropertyService = new StaffPropertyService(env.ObjVerEx.Vault, Configuration);
            int createdByID = env.ObjVerEx.Properties.SearchForProperty((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCreatedBy).TypedValue.GetLookupID();

            //Site Carers
            SetAssignmentUsers(staffPropertyService, Configuration.SiteCarers, env.ObjVerEx, createdByID);

            //Site Independant Administrators
            SetAssignmentUsers(staffPropertyService, Configuration.SiteIndependantAdministrators, env.ObjVerEx, createdByID);

            //Site Administrators
            SetAssignmentUsers(staffPropertyService, Configuration.SiteAdministrators, env.ObjVerEx, createdByID);

            //Site Village Managers
            SetAssignmentUsers(staffPropertyService, Configuration.SiteVillageManagers, env.ObjVerEx, createdByID);

            //Site Care Managers
            SetAssignmentUsers(staffPropertyService, Configuration.SiteCareManagers, env.ObjVerEx, createdByID);

            //Site Team Leaders
            SetAssignmentUsers(staffPropertyService, Configuration.SiteTeamLeaders, env.ObjVerEx, createdByID);

            //Notification Site
            SetNotificationSite(staffPropertyService, env.ObjVerEx, createdByID);

            env.ObjVerEx.SaveProperties();
        }

        private void SetNotificationSite(StaffPropertyService staffPropertyService, ObjVerEx objVerEx, int createdById)
        {
            if (objVerEx.HasProperty(Configuration.Assignments_NotificationSite))
            {
               ObjVerEx staff = staffPropertyService.GetStaffObjVerExForUserId(createdById);
               ObjVerEx site = staffPropertyService.GetSiteObjectFromStaff(staff);

               objVerEx.AddLookup(Configuration.Assignments_NotificationSite, site.ObjVer);
            }
        }

        private void SetAssignmentUsers(StaffPropertyService staffPropertyService, MFIdentifier type, ObjVerEx objVerEx, int createdByID)
        {
            if (objVerEx.HasProperty(type))
            {
                staffPropertyService.GetAssignmentUsersByTypeForCreatedByUser(createdByID, type)
                        .ForEach(x =>
                        {
                            objVerEx.AddLookup(type, x);
                        });
            }
        }
    }
}
