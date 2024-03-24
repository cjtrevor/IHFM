using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [PropertyCustomValue("MFiles.Property.TotalItems", Priority = 100)]
        public TypedValue SetTotalLaundryItemsValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            LaundryCalculationService service = new LaundryCalculationService();

            List<MFIdentifier> configs = new List<MFIdentifier>
            {
                Configuration.Laundry_Baadjies
                ,Configuration.Laundry_BedBeskermings
                ,Configuration.Laundry_BedJakkies
                ,Configuration.Laundry_BedSokkies
                ,Configuration.Laundry_Bloese
                ,Configuration.Laundry_Borslappe
                ,Configuration.Laundry_Bra
                ,Configuration.Laundry_Comforter
                ,Configuration.Laundry_DamesFrokkies
                ,Configuration.Laundry_Dekens
                ,Configuration.Laundry_DriekwartBroeke
                ,Configuration.Laundry_Duvet
                ,Configuration.Laundry_DuvetCovers
                ,Configuration.Laundry_Gordyne
                ,Configuration.Laundry_Handdoeke
                ,Configuration.Laundry_Hemde
                ,Configuration.Laundry_KamerJasse
                ,Configuration.Laundry_Keppies
                ,Configuration.Laundry_Kleedtjies
                ,Configuration.Laundry_Komberse
                ,Configuration.Laundry_KortBroeke
                ,Configuration.Laundry_Kussing
                ,Configuration.Laundry_Kussingslope
                ,Configuration.Laundry_Laken
                ,Configuration.Laundry_LangBroeke
                ,Configuration.Laundry_LangpypOnderbroeke
                ,Configuration.Laundry_Leggings
                ,Configuration.Laundry_MansFrokkies
                ,Configuration.Laundry_Nagrokke
                ,Configuration.Laundry_Onderbroeke
                ,Configuration.Laundry_Oorjasse
                ,Configuration.Laundry_Oorpakbaadjies
                ,Configuration.Laundry_OorpakBroeke
                ,Configuration.Laundry_PajamaTops
                ,Configuration.Laundry_PajamaBroeke
                ,Configuration.Laundry_Panties
                ,Configuration.Laundry_Paslaken
                ,Configuration.Laundry_Rokke
                ,Configuration.Laundry_Rompe
                ,Configuration.Laundry_Sakdoeke
                ,Configuration.Laundry_Servette
                ,Configuration.Laundry_Sokkies
                ,Configuration.Laundry_SweetpakBroeke
                ,Configuration.Laundry_SweetpakTops
                ,Configuration.Laundry_Swembroeke
                ,Configuration.Laundry_Tafeldoeke
                ,Configuration.Laundry_THemde
                ,Configuration.Laundry_Treklaken
                ,Configuration.Laundry_Truie
                ,Configuration.Laundry_WasgoedSakke
        };

            calculated.SetValue(MFDataType.MFDatatypeInteger, service.GetScoreSumFromListValues(env.ObjVerEx, configs));

            return calculated;
        }


        [PropertyCustomValue("MFiles.Property.TotalChargableItems", Priority = 100)]
        public TypedValue SetTotalLaundryTotalChargeableItemsValue(PropertyEnvironment env)
        {
            TypedValue calculated = new TypedValue();

            LaundryCalculationService service = new LaundryCalculationService();

            List<MFIdentifier> configs = new List<MFIdentifier>
            {
                Configuration.Laundry_Baadjies
                ,Configuration.Laundry_BedBeskermings
                ,Configuration.Laundry_BedJakkies
                ,Configuration.Laundry_BedSokkies
                ,Configuration.Laundry_Bloese
                ,Configuration.Laundry_Borslappe
                ,Configuration.Laundry_Comforter
                ,Configuration.Laundry_Dekens
                ,Configuration.Laundry_DriekwartBroeke
                ,Configuration.Laundry_Duvet
                ,Configuration.Laundry_DuvetCovers
                ,Configuration.Laundry_Gordyne
                ,Configuration.Laundry_Handdoeke
                ,Configuration.Laundry_Hemde
                ,Configuration.Laundry_KamerJasse
                ,Configuration.Laundry_Kleedtjies
                ,Configuration.Laundry_Komberse
                ,Configuration.Laundry_KortBroeke
                ,Configuration.Laundry_Kussing
                ,Configuration.Laundry_Kussingslope
                ,Configuration.Laundry_Laken
                ,Configuration.Laundry_LangBroeke
                ,Configuration.Laundry_Leggings
                ,Configuration.Laundry_Nagrokke
                ,Configuration.Laundry_Oorjasse
                ,Configuration.Laundry_Oorpakbaadjies
                ,Configuration.Laundry_OorpakBroeke
                ,Configuration.Laundry_PajamaTops
                ,Configuration.Laundry_PajamaBroeke
                ,Configuration.Laundry_Paslaken
                ,Configuration.Laundry_Rokke
                ,Configuration.Laundry_Rompe
                ,Configuration.Laundry_SweetpakBroeke
                ,Configuration.Laundry_SweetpakTops
                ,Configuration.Laundry_Swembroeke
                ,Configuration.Laundry_Tafeldoeke
                ,Configuration.Laundry_THemde
                ,Configuration.Laundry_Treklaken
                ,Configuration.Laundry_Truie
                ,Configuration.Laundry_WasgoedSakke
        };

            calculated.SetValue(MFDataType.MFDatatypeInteger, service.GetScoreSumFromListValues(env.ObjVerEx, configs));

            return calculated;
        }
    }
}

