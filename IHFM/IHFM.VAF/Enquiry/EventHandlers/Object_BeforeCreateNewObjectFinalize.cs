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
        [EventHandler(MFilesAPI.MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, Class = "MFiles.Class.EnquirySupportDocuments")]
        public void EnquirySupportDocuments_AfterCreateNewObjectFinalize(EventHandlerEnvironment env)
        {
            var docTypeName = env.ObjVerEx.GetProperty(Configuration.EnquirySupportDocuments_SupportDocumentType).TypedValue.DisplayValue;
            var propertyAlias = $"MFiles.Property.{docTypeName.Replace(" ", "").ToLower()}";

            if (env.Vault.PropertyDefOperations.GetPropertyDefIDByAlias(propertyAlias) == -1)
                return;

            var resident = env.ObjVerEx.GetProperty(Configuration.EnquirySupportDocuments_ExistingClient).TypedValue.GetValueAsLookup();

            MFSearchBuilder enquirySearch = new MFSearchBuilder(env.Vault);
            enquirySearch.Class(Configuration.Enquiry_Class);
            enquirySearch.Property(Configuration.Enquiry_ExistingClient, MFDataType.MFDatatypeLookup, resident.Item);
            var enquiryResults = enquirySearch.FindEx();

            foreach (var enquiryItem in enquiryResults)
            {
                enquiryItem.SaveProperty(propertyAlias, MFDataType.MFDatatypeBoolean, true);
            }
        }
    }
}
