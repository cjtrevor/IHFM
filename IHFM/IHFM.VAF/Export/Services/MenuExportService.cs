using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class MenuExportService
    {
        private readonly Configuration configuration;
        private readonly Vault vault;

        public MenuExportService(Configuration _configuration, Vault _vault)
        {
            configuration = _configuration;
            vault = _vault;
        }

        public void ExportMenu(ObjVerEx menu)
        {
            string menuId = menu.ObjID.ID.ToString();
            string startDate = menu.GetProperty(configuration.MenuStartDate).GetValueAsLocalizedText();
            string endDate = menu.GetProperty(configuration.MenuEndDate).GetValueAsLocalizedText();

            string filename = $"Menu {menuId}_{startDate} - {endDate}";
            List<string> lineitems = new List<string>();

            //Starters
            lineitems.Add("Starters");
            lineitems.AddRange(GetLookupText(menu.GetLookups(configuration.MenuStarterMealOptions)));

            //Mains
            lineitems.Add("Mains");
            lineitems.AddRange(GetLookupText(menu.GetLookups(configuration.MenuMainMealOptions)));

            //Sides
            lineitems.Add("Sides");
            lineitems.AddRange(GetLookupText(menu.GetLookups(configuration.MenuSidesMealOptions)));

            //Desserts
            lineitems.Add("Desserts");
            lineitems.AddRange(GetLookupText(menu.GetLookups(configuration.MenuDessertMealOptions)));

            File.WriteAllLines($"C:\\IHFM\\MenuExports\\{filename.Replace(@"/","-")}.txt", lineitems);
        }

        private List<string> GetLookupText(Lookups items)
        {
            List<string> texts = new List<string>();

            foreach(Lookup item in items)
            {
                ObjVerEx itemObj = new ObjVerEx(vault, item);
                string name = itemObj.GetPropertyText(configuration.MealItemName);
                string id = itemObj.ObjID.ID.ToString();

                texts.Add($"0;{name};{id}");
            }

            return texts;
        }
    }
}
