using MFiles.VAF.Configuration;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        [MFPropertyDef]
        public MFIdentifier QRCode_Resident = "MFiles.Property.Resident";

        [MFPropertyDef]
        public MFIdentifier QRCode_RoomNumber_Text = "MFiles.Property.RoomNumber"; //NO ALIAS

        [MFPropertyDef]
        public MFIdentifier QRCode_Room_List = "MFiles.Property.RoomList"; //NO ALIAS //BOTTOM ONE IN SELECT VALUE LIST DROPDOWN




        ////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////      SELF MADE PROPS      ////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////

        [MFPropertyDef]
        public MFIdentifier QRCode_ResRoom_SelfMade = "MFiles.Property.Resroom"; //CHOOSE FROM LIST "ROOMS" //TOP ONE IN SELECT VALUE LIST DROPDOWN
        [MFPropertyDef]
        public MFIdentifier QRCode_QRClass = "MFiles.Property.QrClass"; //REQUIRED

        //Objects
        [MFObjType]
        public MFIdentifier QRCode_DailyCareObject = "MFiles.Object.DailyCare";

        //Classes
        [MFClass]
        public MFIdentifier QRCode_DailyCareObject_HourlyRoundsClass = "MFiles.ClassHourlyRounds";

        [MFValueListItem(ValueList = "MFiles.Valuelist.QrClass")]
        public MFIdentifier QRCode_QRClasses_HourlyRounds = "{299AFF25-E7FC-45BC-81A7-C1CC6280F6A5}";


    }
}
