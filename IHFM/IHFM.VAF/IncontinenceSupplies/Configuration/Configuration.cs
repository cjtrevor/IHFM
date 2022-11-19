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
        [MFObjType(Required = true)]
        public MFIdentifier IncontinenceSupplies_Object = "MFiles.Object.ResidentIncontinenceStock";

        [MFClass(Required = true)]
        public MFIdentifier IncontinenceSupplies_Class = "MFiles.Class.IncontinenceSupplies";
        [MFClass(Required = true)]
        public MFIdentifier IncontinenceSupplies_StockOnHandClass = "MFiles.Class.ResidentIncontinenceStock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier IncontinenceSupplies_IncontinenceProduct = "MFiles.Property.IncontinenceProduct";
        [MFPropertyDef(Required = true)]
        public MFIdentifier IncontinenceSupplies_StockOnHand = "MFiles.Property.IncontinenceStockOnHand";
        [MFPropertyDef(Required = true)]
        public MFIdentifier IncontinenceSupplies_PackageSize = "MFiles.Property.PackageSize1";
        [MFPropertyDef(Required = true)]
        public MFIdentifier IncontinenceSupplies_Quantity = "MFiles.Property.QtyDispensed";
    }
}
