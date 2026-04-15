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
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Priority = 100)]
        public void CheckPinCode(EventHandlerEnvironment env)
        {
            var pinVal = env.ObjVerEx.Properties.GetProperty(Configuration.StaffLookup_StaffPassword);

            if (pinVal == null)
                return;

            if (string.IsNullOrEmpty(pinVal.GetValueAsLocalizedText()))
                throw new Exception($"Staff Password required");

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_Password, MFDataType.MFDatatypeText, pinVal.GetValueAsLocalizedText());
            var staffUserObject = mFSearchBuilder.FindOneEx();

            if (staffUserObject == null)
                throw new Exception($"Incorrect pin");

            env.ObjVerEx.SetProperty(Configuration.CreatedBy, MFDataType.MFDatatypeLookup, staffUserObject.ID);
        }

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.Staff")]
        public void BeforeCheckInChanges_EnsureUniqueStaffPinCode(EventHandlerEnvironment env)
        {
            var staffPinCode = env.ObjVerEx.Properties.GetProperty(Configuration.Staff_Password);

            if (string.IsNullOrWhiteSpace(staffPinCode.GetValueAsLocalizedText()))
                return;

            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(env.ObjVerEx.Vault);
            mFSearchBuilder.Class(Configuration.Staff);
            mFSearchBuilder.Property(Configuration.Staff_Password, MFDataType.MFDatatypeText, staffPinCode.GetValueAsLocalizedText());
            var existingObjectsWithSamePincode = mFSearchBuilder.FindEx();

            if (existingObjectsWithSamePincode.Count > 1)
                throw new Exception($"Invalid password, please try a different password");
        }

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
