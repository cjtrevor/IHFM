using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef]
        public MFIdentifier MaintReq_Resident = "MFiles.Property.Resident";

        [MFPropertyDef]
        public MFIdentifier MaintReq_Staff = "MFiles.Property.Staff";

        [MFPropertyDef]
        public MFIdentifier MaintReq_JobAssignedTo = "MFiles.Property.JobAssignedTo";

        [MFPropertyDef]
        public MFIdentifier MaintReq_JobToBeDone = "MFiles.Property.JobToBeDone";

        [MFPropertyDef]
        public MFIdentifier MaintReq_TimeStarted = "MFiles.Property.TimeStarted";

        [MFPropertyDef]
        public MFIdentifier MaintReq_TimeFinished = "MFiles.Property.TimeFinished";

        [MFPropertyDef]
        public MFIdentifier MaintReq_CommentsNotes = "MFiles.Property.CommentsNotes";

        [MFPropertyDef]
        public MFIdentifier MaintReq_JobStatus = "MFiles.Property.JobStatus";

        [MFPropertyDef]
        public MFIdentifier MaintReq_PrintPDF = "MFiles.Property.GeneratePdfForSignature";

        [MFPropertyDef]
        public MFIdentifier MaintReq_Staff_EmploymentStatus = "MFiles.Property.EmploymentStatus";

        [MFPropertyDef]
        public MFIdentifier MaintReq_Staff_Email = "MFiles.Property.EmailAddressStaff";

        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.EmployeeStatus")]
        public MFIdentifier MaintReq_Staff_ContractorStatus = "{72D62093-59DE-4E0E-9AA6-4A62B18F8163}";
        
    }
}
