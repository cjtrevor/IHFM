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
        [MFPropertyDef(Required = true)]
        public MFIdentifier Vacant = "MFiles.Property.Vacant";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Room_Zone = "MFiles.Property.Zone";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Room_RoomNumber = "MFiles.Property.RoomNumber";

        [MFPropertyDef]
        public MFIdentifier Room_Tariff = "MFiles.Property.RoomTariff";

        [MFObjType(Required = true)]
        public MFIdentifier Room_Object = "MFiles.Object.Room";

        [MFValueList]
        public MFIdentifier Room_TariffValueList = "VL.Roomtariff";
    }
}
