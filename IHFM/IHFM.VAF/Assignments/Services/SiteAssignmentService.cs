using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Extensions;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class SiteAssignmentService
    {
        private readonly Configuration configuration;

        public SiteAssignmentService(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void SetSiteAssignmentProperties(EventHandlerEnvironment env)
        {
            if (env.ObjVerEx.Class == 1180
                || env.ObjVerEx.Class == configuration.Site_Class.ID
                || env.ObjVerEx.Class == configuration.Staff.ID
                || env.ObjVerEx.Class == configuration.MDDAuto_Class.ID)
                return;

            StaffPropertyService staffPropertyService = new StaffPropertyService(env.ObjVerEx.Vault, configuration);
            int createdByID = env.ObjVerEx.Properties.SearchForProperty((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCreatedBy).TypedValue.GetLookupID();

            //Site Carers
            SetAssignmentUsers(staffPropertyService, configuration.SiteCarers, env.ObjVerEx, createdByID);

            //Site Independant Administrators
            SetAssignmentUsers(staffPropertyService, configuration.SiteIndependantAdministrators, env.ObjVerEx, createdByID);

            //Site Administrators
            SetAssignmentUsers(staffPropertyService, configuration.SiteAdministrators, env.ObjVerEx, createdByID);

            //Site Village Managers
            SetAssignmentUsers(staffPropertyService, configuration.SiteVillageManagers, env.ObjVerEx, createdByID);

            //Site Care Managers
            SetAssignmentUsers(staffPropertyService, configuration.SiteCareManagers, env.ObjVerEx, createdByID);

            //Site Team Leaders
            SetAssignmentUsers(staffPropertyService, configuration.SiteTeamLeaders, env.ObjVerEx, createdByID);

            if (env.ObjVerEx.HasProperty(configuration.PanicButtonTest_ReportToMaintenanceManager))
            {
                var reportToMaintenanceManager = env.ObjVerEx.GetPropertyAsBoolean(configuration.PanicButtonTest_ReportToMaintenanceManager);

                if (reportToMaintenanceManager ?? false)
                {
                    if (env.ObjVerEx.HasProperty(configuration.BaseSite) && env.ObjVerEx.HasValue(configuration.BaseSite))
                    {
                        var siteLookup = env.ObjVerEx.GetProperty(configuration.BaseSite).TypedValue.GetValueAsLookup();
                        var site = new ObjVerEx(env.Vault, siteLookup);

                        var siteMaintenanceManagers = site.GetLookupsFromProperty(configuration.Site_SiteMaintenanceManagers)
                            .Select(x => x.GetAsObjVer())
                            .ToList();

                        foreach (var item in siteMaintenanceManagers)
                        {
                            env.ObjVerEx.AddLookup(configuration.Site_SiteMaintenanceManagers, item);
                        }
                    }
                    else
                    {
                        staffPropertyService.GetAssignmentUsersByTypeForCreatedByUser(createdByID, configuration.Site_SiteMaintenanceManagers)
                            .ForEach(x =>
                            {
                                env.ObjVerEx.AddLookup(configuration.Site_SiteMaintenanceManagers, x);
                            });
                    }
                }
            }

            //Notification Site
            SetNotificationSite(staffPropertyService, env.ObjVerEx, createdByID);

            env.ObjVerEx.SaveProperties();
        }

        private void SetNotificationSite(StaffPropertyService staffPropertyService, ObjVerEx objVerEx, int createdById)
        {
            if (objVerEx.HasProperty(configuration.Assignments_NotificationSite))
            {
                ObjVerEx staff = staffPropertyService.GetStaffObjVerExForUserId(createdById);
                ObjVerEx site = staffPropertyService.GetSiteObjectFromStaff(staff);

                objVerEx.AddLookup(configuration.Assignments_NotificationSite, site.ObjVer);
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
