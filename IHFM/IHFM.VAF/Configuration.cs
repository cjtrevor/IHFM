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
        public MFIdentifier Item1Stock = "MFiles.Property.Item1Stock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item2Stock = "MFiles.Property.Item2Stock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item3Stock = "MFiles.Property.Item3Stock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item4Stock = "MFiles.Property.Item4Stock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item5Stock = "MFiles.Property.Item5Stock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier StockQuantityIssued = "MFiles.Property.StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item1StockQuantityIssued = "MFiles.Property.Item1StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item2StockQuantityIssued = "MFiles.Property.Item2StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item3StockQuantityIssued = "MFiles.Property.Item3StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item4StockQuantityIssued = "MFiles.Property.Item4StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Item5StockQuantityIssued = "MFiles.Property.Item5StockQuantityIssued";

        [MFPropertyDef(Required = true)]
        public MFIdentifier StockOnHand = "MFiles.Property.StockOnHand";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Site = "MFiles.Property.Site";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteList = "MFiles.Properties.SiteList";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TranspharmStockSite = "MFiles.Properties.TranspharmStockSite";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TranspharmStock = "MFiles.Property.TranspharmStock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Transfer = "MFiles.Property.StockInOrOut";

    }
}