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
        [MFObjType]
        public MFIdentifier MDDAuto_MDDObjectId = "OT.Medsdosagedispense";

        [MFPropertyDef]
        public MFIdentifier MDDAuto_Resident = "MFiles.Property.Resident";
        [MFPropertyDef]
        public MFIdentifier MDDAuto_MedsOnScript = "Mfiles.Property.MedsOnScript";
        [MFPropertyDef]
        public MFIdentifier MDDAuto_MDDValues = "MFiles.Property.Mddvalues";

        [MFClass]
        public MFIdentifier MDDAuto_Class = "MFiles.Class.MedsGivenAuto";
    }
}
