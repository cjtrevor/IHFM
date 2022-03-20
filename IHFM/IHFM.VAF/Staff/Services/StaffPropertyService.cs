using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class StaffPropertyService
    {
        private Vault _vault;
        private Configuration _configuration;
        public StaffPropertyService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public List<ObjVer> GetAssignmentUsersByTypeForCreatedByUser(int userId, MFIdentifier assignmentProperty)
        {
            ObjVerEx staff = GetStaffObjVerExForUserId(userId);

            return GetSiteObjectFromStaff(staff).GetLookupsFromProperty(assignmentProperty)
                .Select(x=> x.GetAsObjVer())
                .ToList();
        }

        public ObjVerEx GetSiteObjectFromStaff(ObjVerEx staff)
        {
            Lookup site = staff.GetProperty(_configuration.Assignments_Site).TypedValue.GetValueAsLookup();

            return new ObjVerEx(_vault, site);
        }

        public ObjVerEx GetStaffObjVerExForUserId(int userId)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.Class(_configuration.Staff);
            mFSearchBuilder.Property(_configuration.Login, MFDataType.MFDatatypeLookup, userId);
            ObjectSearchResults objectSearchResults = mFSearchBuilder.Find();

            if (objectSearchResults.Count == 0)
            {
                throw new Exception("No staff object exists for the current user");
            }

            return mFSearchBuilder.FindOneEx();
        }
    }
}
