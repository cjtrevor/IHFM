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

        [MFPropertyDef]
        public MFIdentifier DailyCare_CarePlanNotes = "MFiles.Property.CarePlanNotes";

        [MFPropertyDef(Required = true)]
        public MFIdentifier ProgressName_ProgressNoteDet = "MFiles.Property.ProgressNoteDet";

        [MFPropertyDef]
        public MFIdentifier TBCS_CompletedCare = "MFiles.Property.CompletedCare";

        [MFPropertyDef]
        public MFIdentifier TBCS_0600_1000Care = "MFiles.Property.0600_1000Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1000_1400Care = "MFiles.Property.1000_1400Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1400_1800Care = "MFiles.Property.1400_1800Care";

        //[MFPropertyDef]
        //public MFIdentifier TBCS_TbcScheduledTimes = "MFiles.Property.TbcScheduledTimes";

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

        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0600 = "{D641088F-F3A3-402A-B4AF-FF8C7CBBF9F0}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0800 = "{B573B435-0BCE-47E7-B1A0-129AABBC6467}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1000 = "{C13FDCB7-887A-47E9-9A15-EE564D6AAB85}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1200 = "{650243C7-16D4-49F1-A317-3CB5DA3341FD}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1400 = "{520332F8-814C-4D6C-B6F4-1A721AB9569D}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1600 = "{B4334103-CFA0-41AD-931A-9C5AF53C2A4B}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1800 = "{E45C6E8E-CBEB-4B50-BC18-A57AE54AD72D}";

    }
}

