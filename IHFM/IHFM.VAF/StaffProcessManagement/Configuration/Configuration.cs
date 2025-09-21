using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef(Required = true)]
        public MFIdentifier StaffProcessManagement_Staffs = "MFiles.Property.Staffs";
        [MFPropertyDef(Required = true)]
        public MFIdentifier StaffProcessManagement_PolicyDocuments = "MFiles.Property.PolicyDocument";
        [MFPropertyDef(Required = true)]
        public MFIdentifier StaffProcessManagement_CommentsNotes = "MFiles.Property.CommentsNotes";
    }
}
