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
        public MFIdentifier MealItemName = "MFiles.Property.MealItemName";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MenuStartDate = "MFiles.Property.MenuStartDate";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MenuEndDate = "MFiles.Property.MenuEndDate";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MenuStarterMealOptions = "MFiles.Property.StartersMealOptions";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MenuMainMealOptions = "MFiles.Property.MainMealOptions";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MenuSidesMealOptions = "MFiles.Property.SidesMealOptions";
        [MFPropertyDef(Required = true)]
        public MFIdentifier MenuDessertMealOptions = "MFiles.Property.DessertMealOptions";
    }
}
