using MFiles.VAF.Configuration;
using System;
using System.Runtime.Serialization;

namespace IHFM.VAF
{
    [DataContract]
    public class Configuration
    {
        //Object Aliases
        #region Site
        [MFObjType(Required = true)]
        public MFIdentifier SiteStockObject = "MFiles.Object.SiteStock";
        #endregion

        //Class Aliases
        #region Stock
        [MFClass(Required = true)]
        public MFIdentifier SiteStock = "MFiles.Class.SiteStock";
        #endregion

        #region Site
        [MFClass(Required = true)]
        public MFIdentifier Staff = "MFiles.Class.Staff";
        #endregion

        //Property Aliases
        #region Stock
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
        public MFIdentifier TranspharmStock = "MFiles.Property.TranspharmStock";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Transfer = "MFiles.Property.StockInOrOut";
        #endregion

        #region Site
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site = "MFiles.Property.Site";

        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteList = "MFiles.Properties.SiteList";

        [MFPropertyDef(Required = true)]
        public MFIdentifier BaseSiteID = "MFiles.Property.BaseSiteId";

        [MFPropertyDef(Required = true)]
        public MFIdentifier BaseSite = "MFiles.Property.BaseSite";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TranspharmStockSite = "MFiles.Properties.TranspharmStockSite";

        [MFPropertyDef(Required = true)]
        public MFIdentifier Login = "MFiles.Property.Login";
        #endregion

        //Custom Properties
        #region
        [DataMember]
        [JsonConfEditor(Label = "Assignment Creation Watermark", 
                        HelpText = "This indicates the next time auto assignment creation will be triggered",
                        TypeEditor = "timestamp")]
        public DateTime AssignmentCreationWatermark { get; set; } 
        #endregion
    }
}