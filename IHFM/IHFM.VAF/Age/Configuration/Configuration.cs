using MFiles.VAF.Configuration;
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
        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier IDNumber = "MFiles.Property.IDNumber";
        [MFPropertyDef]
        public MFIdentifier StaffIDNumber = "MFiles.Property.StaffIdNumber";
        [MFPropertyDef(Required = true)]
        public MFIdentifier Age = "MFiles.Property.Age";
        [MFPropertyDef(Required = true)]
        public MFIdentifier AverageSiteAge = "MFiles.Property.AverageSiteAge";
        [MFPropertyDef]
        public MFIdentifier DobDay = "MFiles.Property.DobDay";
        [MFPropertyDef]
        public MFIdentifier DobMonth = "MFiles.Property.DobMonth";

        //Class Aliases
        [MFClass]
        public MFIdentifier Age_ResidentClass = "MFiles.Class.Resident";
        [MFClass]
        public MFIdentifier Age_StaffClass = "MFiles.Class.Staff";


        //Admin Configurations
        [DataMember]
        [JsonConfIntegerEditor(DefaultValue = 60, HelpText = "Interval for refreshing resident ages in hours")]
        public int AgeRunCheckInterval { get; set; }

        [DataMember]
        [JsonConfIntegerEditor(DefaultValue = 30, HelpText = "Interval for refreshing average site ages in hours")]
        public int SiteAverageAgeRunCheckInterval { get; set; }
    }
}
