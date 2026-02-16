using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef]
        public MFIdentifier QRCode_Resident = "MFiles.Property.Resident";

        //Objects
        [MFObjType]
        public MFIdentifier QRCode_DailyCareObject = "MFiles.Object.DailyCare";

        //Classes
        [MFClass]
        public MFIdentifier QRCode_DailyCareObject_HourlyRoundsClass = "MFiles.Class.HourlyRounds_QR";


    }
}
