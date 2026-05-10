using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        public MFIdentifier Enquiry_Class = "MFiles.Class.Enquiry";

        public MFIdentifier Enquiry_ExistingClient = "MFiles.Property.ExistingClient";
        public MFIdentifier EnquirySupportDocuments_ExistingClient = "MFiles.Property.ExistingClient";
        public MFIdentifier EnquirySupportDocuments_SupportDocumentType = "MFiles.Property.SupportDocumentType";
    }
}
