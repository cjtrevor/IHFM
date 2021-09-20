﻿using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Object Aliases
        [MFObjType(Required = true)]
        public MFIdentifier SiteStockObject = "MFiles.Object.SiteStock";

        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier SiteStock = "MFiles.Class.SiteStock";

        //Property Aliases
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
    }
}