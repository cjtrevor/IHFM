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
    public class SitePermissionService
    {

        private Vault _vault;
        private Configuration _configuration;
        public SitePermissionService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void SetSiteFromStaffByUserID(ObjVerEx objVerEx, MFIdentifier siteProperty)
        {
            int createdByID = objVerEx.Properties.SearchForProperty((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefCreatedBy).TypedValue.GetLookupID();
            int siteID = GetSiteIDFromStaffByUserID(createdByID);

            objVerEx.SetProperty(siteProperty, MFDataType.MFDatatypeLookup, siteID);
            objVerEx.SaveProperties();
        }

        public string GetSiteNumByUserID(int userID)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.Class(_configuration.Staff);
            mFSearchBuilder.Property(_configuration.Login, MFDataType.MFDatatypeLookup, userID);
            ObjectSearchResults objectSearchResults = mFSearchBuilder.Find();

            if (objectSearchResults.Count == 0)
            {
                throw new Exception("No staff object exists for the current user");
            }

            ObjVerEx staff = mFSearchBuilder.FindOneEx();
            return GetSiteNum(staff);
        }

        private string GetSiteNum(ObjVerEx objVerEx)
        {
            Lookup siteLookup = objVerEx.GetProperty(_configuration.BaseSite).TypedValue.GetValueAsLookup();
            string siteGuid = siteLookup.ItemGUID;
            ObjID siteObjID = _vault.ObjectOperations.GetObjIDByGUID(siteGuid);
            ObjectVersionAndProperties objectVersionAndProperties = _vault.ObjectOperations.GetLatestObjectVersionAndProperties(siteObjID, true);

            return objectVersionAndProperties.Properties.GetProperty(_configuration.BaseSiteID.ID).TypedValue.GetValueAsLocalizedText();
        }

        public int GetSiteIDFromStaffByUserID(int userID)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.Class(_configuration.Staff);
            mFSearchBuilder.Property(_configuration.Login, MFDataType.MFDatatypeLookup, userID);
            ObjectSearchResults objectSearchResults = mFSearchBuilder.Find();

            if (objectSearchResults.Count == 0)
            {
                throw new Exception("No staff object exists for the current user");
            }

            ObjVerEx staff = mFSearchBuilder.FindOneEx();

            return GetSiteID(staff);
        }

        private int GetSiteID(ObjVerEx objVerEx)
        {
            Lookup siteLookup = objVerEx.GetProperty(_configuration.BaseSite).TypedValue.GetValueAsLookup();
            string siteGuid = siteLookup.ItemGUID;
            ObjID siteObjID = _vault.ObjectOperations.GetObjIDByGUID(siteGuid);
            ObjectVersionAndProperties objectVersionAndProperties = _vault.ObjectOperations.GetLatestObjectVersionAndProperties(siteObjID, true);

            return objectVersionAndProperties.Properties.GetProperty(_configuration.BaseSiteID.ID).TypedValue.GetLookupID();
        }
    }
}
