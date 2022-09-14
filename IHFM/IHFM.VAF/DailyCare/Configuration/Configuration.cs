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
        public MFIdentifier DailyCare_BowelMovement = "MFiles.Properties.BowelMovement";

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

        [MFPropertyDef(Required = true)]
        public MFIdentifier ProgressName_ProgressNoteDet = "MFiles.Property.ProgressNoteDet";

        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier DailyCareClass = "MFiles.Class.DailyCare";

        //ValueListItem Aliases
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_AdmissionNoteType = "{FDB118DC-EA9A-4932-940B-1A29902128C2}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_IncidentNoteType = "{61CFF907-0DBB-43F4-B7AB-2F8FD14D7261}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_DeceasedNoteType = "{C879C17A-0805-410F-8BAC-7BCC286ECC88}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_DischargedNoteType = "{B8B59A88-1972-4CA0-AC46-E1ECC1BACA88}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_HospitalNoteType = "{513BC17D-9822-4918-8EE9-52EA87238B16}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_TempDischargeNoteType = "{18733FEB-189D-4F73-826B-BCBF41A94B53}";
        [MFValueListItem(Required = true, ValueList = "MFiles.ValueList.ProgressNoteType")]
        public MFIdentifier DailyCare_BackInResidenceNoteType = "{6CC30B8A-B93D-4FD3-97BF-79F94B3F2F17}";

    }
}
