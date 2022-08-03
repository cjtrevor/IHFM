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
        //Property Aliases
        [MFPropertyDef(Required = true)]
        public MFIdentifier CareDoneForShift = "MFiles.Property.CareDoneForShift";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_NoteType = "MFiles.Property.NoteType";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_Site = "MFiles.Properties.SiteList";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_Resident = "MFiles.Property.Resident";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_BowelMovement = "MFiles.Property.BowelMovement";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_BathType = "MFiles.Property.BathType";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_HadBreakfast = "MFiles.Property.HadBreakfast";
        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_HadLunch = "MFiles.Property.HadLunch";
        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_HadSupper = "MFiles.Property.HadSupper";

        [MFPropertyDef(Required = true)]
        public MFIdentifier DailyCare_IsComplete = "MFiles.Property.isComplete";
        

        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier DailyCareClass = "MFiles.Class.DailyCare";

        //ValueListItem Aliases
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_AdmissionNoteType = "{FDB118DC-EA9A-4932-940B-1A29902128C2}";

    }
}
