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

    }
}
