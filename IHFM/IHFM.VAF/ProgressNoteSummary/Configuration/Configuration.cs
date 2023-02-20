using MFiles.VAF.Configuration;
namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFObjType]
        public MFIdentifier ProgressNoteSummary_Object = "MFiles.Object.ProgressNoteSummary";

        [MFClass]
        public MFIdentifier ProgressNoteSummary_Class = "MFiles.Class.ProgressNoteSummary";

        [MFPropertyDef]
        public MFIdentifier ProgressNoteSummary_BaseSite = "MFiles.Properties.VAFSite";
        [MFPropertyDef]
        public MFIdentifier ProgressNoteSummary_Site = "MFiles.Property.BaseSite";
        [MFPropertyDef]
        public MFIdentifier ProgressNoteSummary_Month = "PD.Montht";
        [MFPropertyDef]
        public MFIdentifier ProgressNoteSummary_Year = "PD.Year";
        [MFPropertyDef]
        public MFIdentifier ProgressNoteSummary_NoteType = "MFiles.Property.NoteType";
        [MFPropertyDef]
        public MFIdentifier ProgressNoteSummary_ProgressNoteCount = "MFiles.Property.ProgressNoteCount";
    }
}
