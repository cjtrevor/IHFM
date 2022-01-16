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
        //Property Definitions

        [MFPropertyDef(Required = true)]
        public MFIdentifier MedsOnScript = "Mfiles.Property.MedsOnScript";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds0600 = "Mfiles.Property.0600?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds0900 = "Mfiles.Property.0900?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds1200 = "Mfiles.Property.1200?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds1700 = "Mfiles.Property.1700?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier GiveMeds2000 = "Mfiles.Property.2000?";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ScriptManagementEndDate = "MFiles.Property.ScriptManagementEndDate";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TestHour = "MFiles.Property.Testhour";

        [MFPropertyDef(Required = true)]
        public MFIdentifier TestMinute = "MFiles.Property.Testminute";

        //Class Definitions
        [MFClass(Required = true)]
        public MFIdentifier ScriptManagementClass = "MFiles.Class.ScriptManagement";

        [MFClass(Required = true)]
        public MFIdentifier MedsGiven = "MFiles.Class.MedsGiven";

        //Admin Configurations

        [DataMember]
        [JsonConfIntegerEditor(DefaultValue = 30, HelpText = "The amount of time to allow before/after a scheduled med slot to be included in meds given.")]
        public int ScriptControlTimeThreshold { get; set; }
    }
}