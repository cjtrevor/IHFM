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
        [MFPropertyDef]
        public MFIdentifier EventsactivitiesNotes = "MFiles.Property.EventsactivitiesNotes";
        [MFPropertyDef(Required = true)]
        public MFIdentifier ProgressName_ProgressNoteDet = "MFiles.Property.ProgressNoteDet";
        


        [MFPropertyDef]
        public MFIdentifier TBCS_CompletedCare = "MFiles.Property.CompletedCare";

        [MFPropertyDef]
        public MFIdentifier TBCS_0000_0100Care = "MFiles.Property.00000100Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0100_0200Care = "MFiles.Property.01000200Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0200_0300Care = "MFiles.Property.02000300Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0300_0400Care = "MFiles.Property.03000400Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0400_0500Care = "MFiles.Property.04000500Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0500_0600Care = "MFiles.Property.05000600Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0600_0700Care = "MFiles.Property.06000700Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0700_0800Care = "MFiles.Property.07000800Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0800_0900Care = "MFiles.Property.08000900Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_0900_1000Care = "MFiles.Property.09001000Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1000_1100Care = "MFiles.Property.10001100Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1100_1200Care = "MFiles.Property.11001200Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1200_1300Care = "MFiles.Property.12001300Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1300_1400Care = "MFiles.Property.13001400Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1400_1500Care = "MFiles.Property.14001500Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1500_1600Care = "MFiles.Property.15001600Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1600_1700Care = "MFiles.Property.16001700Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1700_1800Care = "MFiles.Property.17001800Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_1800_1900Care = "MFiles.Property.18001900Care";    
        [MFPropertyDef]
        public MFIdentifier TBCS_1900_2000Care = "MFiles.Property.19002000Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_2000_2100Care = "MFiles.Property.20002100Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_2100_2200Care = "MFiles.Property.21002200Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_2200_2300Care = "MFiles.Property.22002300Care";
        [MFPropertyDef]
        public MFIdentifier TBCS_2300_0000Care = "MFiles.Property.23000000Care";

        [MFPropertyDef]
        public MFIdentifier TBCS_Frequency = "MFiles.Property.Frequency";

        //[MFPropertyDef]
        //public MFIdentifier TBCS_TbcScheduledTimes = "MFiles.Property.TbcScheduledTimes";

        //Class Aliases
        [MFClass(Required = true)]
        public MFIdentifier DailyCare_DailyCareClass = "MFiles.Class.DailyCare";
        [MFClass(Required = true)]
        public MFIdentifier DailyCare_CareClass = "MFiles.Class.DailyCareCopy";

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
        public MFIdentifier ScheduledCareTime_0000 = "{05C0DE62-AB74-4FD6-8501-8A0C79A67E69}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0100 = "{AE9C7E63-7F7D-48AA-BC13-46C8EB9083ED}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0200 = "{68354390-E24A-43E8-B536-D9EA880662A8}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0300 = "{80FC4A0D-66B9-4F4A-A2C7-E20C9E8B3713}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0400 = "{7AFFBF81-9CCB-4AE6-8B1B-2B806EDFC66E}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0500 = "{DAC5452D-CF27-4E04-B8F5-939DCE5B4A98}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0600 = "{D641088F-F3A3-402A-B4AF-FF8C7CBBF9F0}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0700 = "{F4DD8F6A-7A56-4310-8192-223AC045A93E}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0800 = "{B573B435-0BCE-47E7-B1A0-129AABBC6467}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0900 = "{E554B184-8004-4F3F-9C38-B336C4121409}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1000 = "{C13FDCB7-887A-47E9-9A15-EE564D6AAB85}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1100 = "{0818204D-4BC6-40B3-99AE-925D83703968}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1200 = "{650243C7-16D4-49F1-A317-3CB5DA3341FD}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1300 = "{095E0661-40C8-4B16-B038-66EBBA51E1FF}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1400 = "{520332F8-814C-4D6C-B6F4-1A721AB9569D}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1500 = "{B4B1C762-0242-453F-A4B7-37E184D1C515}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1600 = "{B4334103-CFA0-41AD-931A-9C5AF53C2A4B}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1700 = "{BA2832EB-6567-4BD1-A53E-EECEA3519BF3}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1800 = "{E45C6E8E-CBEB-4B50-BC18-A57AE54AD72D}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1900 = "{3639F5F0-31BD-4DD2-BAD5-CF1FD3856011}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2000 = "{DDCFD0F5-52D0-4B8D-83D0-8D60516D797B}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2100 = "{321F69F3-958E-4A31-AD7B-4CC0C4DE03B1}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2200 = "{E1AB9D17-6767-4506-93EE-79B63B3CADA3}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2300 = "{CD1AED45-FCB4-4646-8391-4640CAB3FF9A}";

        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_SpecificTimes = "{1EB2646E-1FDD-42B6-9F97-B6DFF227FA00}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Hourly = "{FCBDB036-4154-4168-90C2-F429F16FA44F}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_2Hourly = "{E88D695A-88F7-4A47-8913-1BF52F5D85A8}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_3Hourly = "{8EE0AAD5-E9AE-4773-B18A-295465E8C5D6}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_4Hourly = "{F6F145EE-1308-4F7C-9A00-C371570001C5}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_6Hourly = "{DC4302EC-8179-4C16-A00E-8FB36FF2AB0E}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_8Hourly = "{FC5EC8DF-9115-421C-8907-2A832547470C}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Daily = "{DC743AC4-05F9-4FB3-AF6D-02C5D2B79713}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Weekly = "{AB2F45CA-4A79-42CD-B593-92470D1087AD}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Monthly = "{239936C2-65D6-4BA1-92A8-D6FB694C0072}";

        
    }


}


