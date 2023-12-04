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

        [MFPropertyDef]
        public MFIdentifier DailyCare_CarePlanNotes = "MFiles.Property.CarePlanNotes";

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
        public MFIdentifier ScheduledCareTime_0000 = "{9C00FD34-5F17-4861-8ABE-3D9FFCFFD51E}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0100 = "{BF01A0B7-FA85-4F1F-98AF-10D6B8DB1B4B}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0200 = "{FFC5620E-0D97-4F48-A40F-0A8C05DB8235}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0300 = "{DF6C0B79-E23D-496D-968D-CBC46F88C997}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0400 = "{191761E3-5FB7-47A2-B32E-5DE79E176C5E}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0500 = "{9D5D84BE-B548-4D3A-87CA-11BEA23F7090}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0600 = "{64C2DC19-9AD0-4603-975D-64FE93220E05}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0700 = "{64C627A0-FD03-4DDC-BE39-D5DB3657E651}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0800 = "{A5A9C230-EF73-4C8D-9186-1990FD1731E6}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_0900 = "{D147D48F-0B1E-48A3-9879-23BF58A1C968}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1000 = "{5AAF2982-E1BC-4C2C-89C4-A97EC1E8DF28}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1100 = "{0D04692C-E138-46BC-8735-622C1D04D205}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1200 = "{69F68F86-21CE-4E4C-BB25-173AB1E003B2}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1300 = "{ED885BB2-D7FF-4953-BDBA-F41E0220ACFC}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1400 = "{89C31058-FA96-49B4-BABC-D31730C9E1D6}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1500 = "{84612A76-64B9-435F-89E3-3753894F0FED}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1600 = "{E78B2339-540F-4F15-96B3-BC4F4A54E129}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1700 = "{46087BD5-74F0-4AAA-9A06-BD97D5E4F9F9}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1800 = "{775D6062-344A-4ECD-8BE1-FA1C25262537}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_1900 = "{E1A4E20A-D5F7-4161-BC25-87AF166460A6}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2000 = "{83713AEC-08E7-41AF-8628-F3C069253750}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2100 = "{FAFAB5B4-BF59-4393-8642-F46ABD46F7B9}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2200 = "{FFFC7752-5AB7-4B8F-AB00-27A0ADC159F1}";
        [MFValueListItem(ValueList = "MFiles.Property.TbcScheduledTimes")]
        public MFIdentifier ScheduledCareTime_2300 = "{834A8B36-53F2-4205-951A-D718BD34750D}";

        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_SpecificTimes = "{E33D8F4A-ABE3-4EEF-89ED-FA9E55E01A02}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Hourly = "{E16A8F19-5915-49FB-B6A0-2A6CF81D058C}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_2Hourly = "{B4195B0A-2843-4F00-97BF-96E8CCB3D62B}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_3Hourly = "{65C77296-12D5-4A77-A8F9-714C4632F17D}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_4Hourly = "{A0952BBD-4BFA-439B-B4BA-C7B54AA02AF4}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_6Hourly = "{233AD1D4-A913-4BCE-AA83-1600361054CB}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_8Hourly = "{C17DCEB7-A94A-4CBD-AFA5-CBA5F4EE63F6}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Daily = "{92115D42-2ABD-4B26-9B14-43CDF4E98CC2}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Weekly = "{FA7F30EE-B3BC-4318-915D-8C87B00DF6DD}";
        [MFValueListItem(ValueList = "MFiles.Valuelist.ScheduleFrequency")]
        public MFIdentifier Frequency_Monthly = "{F43CD844-3DDB-42BF-826A-2FEE01ECF0E7}" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "" +
            "";
    }
}
