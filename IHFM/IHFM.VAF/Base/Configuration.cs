using MFiles.VAF.Configuration;
using System.Runtime.Serialization;

namespace IHFM.VAF
{
    [DataContract]
    public partial class Configuration
    {
        public MFIdentifier devTestClass = "MFiles.Class.Triggertest";


        public MFIdentifier devTest_Resident = "MFiles.Property.Resident";

        public MFIdentifier ResDocs_Class = "MFiles.Class.ResidentDocuments";
        public MFIdentifier ResDocs_DocumentType = "MFiles.Property.Documenttypesreq";
        public MFIdentifier ResDocs_Resident = "MFiles.Property.Resident";



    }
}