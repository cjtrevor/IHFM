﻿using MFiles.VAF.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class Configuration
    {
        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier Staff = "MFiles.Class.Staff";

        [MFClass]
        public MFIdentifier Site_Config_Class = "MFiles.Class.SiteConfig";

        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site = "MFiles.Property.Site";
        [MFPropertyDef(Required = true)]
        public MFIdentifier SiteList = "MFiles.Properties.SiteList";
        [MFPropertyDef(Required = true)]
        public MFIdentifier BaseSiteID = "MFiles.Property.BaseSiteId";
        [MFPropertyDef(Required = true)]
        public MFIdentifier BaseSite = "MFiles.Property.BaseSite";
        [MFPropertyDef(Required = true)]
        public MFIdentifier VAFSite = "MFiles.Properties.VAFSite";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Login = "MFiles.Property.Login";
        [MFPropertyDef(Required = true)]
        public MFIdentifier NumOfResidents = "MFiles.Property.NumOfResidents";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_Variance = "MFiles.Property.SiteVariance";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_ActualIncome = "MFiles.Property.SiteActualIncome";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_FullIncome = "MFiles.Property.SiteFullIncome";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_VacantBeds = "MFiles.Property.SiteVacantBeds";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_OccupiedBeds = "MFiles.Property.SiteOccupiedBeds";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_TotalBeds = "MFiles.Property.SiteTotalBeds";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_LastDataUpdate = "MFiles.Property.LastDataUpdate";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_ReportSite = "MFiles.Property.ReportSite";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Site_BaseSiteDropdown = "MFiles.Property.BaseSite";

        [MFPropertyDef]
        public MFIdentifier SiteConfig_LevyIncrPerc = "MFiles.Property.Levyincrperc";

        //Object Aliases
        [MFObjType(Required = true)]
        public MFIdentifier SiteObject = "MFiles.Object.Site";
        [MFObjType]
        public MFIdentifier SiteConfig_Object = "MFiles.Object.SiteConfig";

        //ValuelistItems
        [MFValueListItem(Required = true, ValueList = "MFiles.Valuelist.Zones")]
        public MFIdentifier Zone_FrailCareItem = "{42786F8D-3A1E-455F-BAA2-00198C37AA8A}";
        [MFValueListItem(Required = true, ValueList = "MFiles.Valuelist.Zones")]
        public MFIdentifier Zone_MemoryCareItem = "{9E6F2095-23BD-420F-B10F-631B49749D3A}";
        [MFValueListItem(Required = true, ValueList = "MFiles.Valuelist.Zones")]
        public MFIdentifier Zone_SickBeds = "{596E112D-020C-4AA7-A15C-886A0C11B06E}";
        [MFValueListItem(Required = true, ValueList = "MFiles.Valuelist.Zones")]
        public MFIdentifier Zone_Independant = "{D9F2F259-A3C1-464B-912D-FF2A5072D8B7}";

        //Task Configs
        [DataMember]
            [JsonConfIntegerEditor(DefaultValue = 12, HelpText = "Interval for refreshing site nominals in hours")]
            public int SiteNominalRunCheckInterval { get; set; } = 12;

        //Admin Configs
        [DataMember]
        [MFClass]
        [JsonConfEditor]
        [ValueOptions(typeof(ClassTypesOptionsProvider))]
        public IEnumerable<MFIdentifier> CreateByUserIdSites { get; set; }
    }
}
