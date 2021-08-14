using MFiles.VAF.Configuration;
using System.Runtime.Serialization;

namespace IHFM.VAF
{
    [DataContract]
    public class Configuration
    {
        [MFObjType(Required = true)]
        public MFIdentifier SiteStockObject = "MFiles.Object.SiteStock";

        [MFClass(Required = true)]
        public MFIdentifier SiteStock = "MFiles.Class.SiteStock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Stock = "MFiles.Property.Stock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier StockOnHand = "MFiles.Property.StockOnHand";

        [MFPropertyDef(Required = true)]
        public MFIdentifier StockQuantityIssued = "MFiles.Property.StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Site = "MFiles.Property.Site";

    }
}