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
        //Object Aliases
        [MFObjType(Required = true)]
        public MFIdentifier IntakeOutputTotal = "MFiles.Object.IntakeOutputTotal";

        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier IntakeOutputTotalClass = "MFiles.Class.InputOutputTotal";

        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier ShiftIO = "MFiles.Property.ShiftIO";
        [MFPropertyDef(Required = true)]
        public MFIdentifier IntakeTotal = "MFiles.Property.IntakeTotal";
        [MFPropertyDef(Required = true)]
        public MFIdentifier OutputUrine = "MFiles.Property.OutputUrine";
        [MFPropertyDef(Required = true)]
        public MFIdentifier OutputOther = "MFiles.Property.OutputOther";
        [MFPropertyDef(Required = true)]
        public MFIdentifier OutputDiarrhea = "MFiles.Property.OutputDiarrhea";
        [MFPropertyDef(Required = true)]
        public MFIdentifier VolumeIn = "MFiles.Property.VolumeIn";
        [MFPropertyDef(Required = true)]
        public MFIdentifier TypeOfOutput = "MFiles.Property.TypeOfOutput";
        [MFPropertyDef(Required = true)]
        public MFIdentifier VolumeOut = "MFiles.Property.VolumeOut";
        [MFPropertyDef(Required = true)]
        public MFIdentifier TypeOfIntake = "MFiles.Property.TypeOfIntake";

        //ValueListItems
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.IoOutputTypes")]
        public MFIdentifier TypeOfOutputUrine = "{F8BE4B04-340B-480E-A0DF-1D463773E0B5}";

        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.IoOutputTypes")]
        public MFIdentifier TypeOfOutputDiarrhea = "{567476AB-8F71-4C22-AB03-46BB18A74436}";
    }
}
