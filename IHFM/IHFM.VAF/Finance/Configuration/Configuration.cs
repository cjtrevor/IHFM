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
        [MFPropertyDef]
        public MFIdentifier Finance_Lodgings = "MFiles.Property.RoomTariff";
        [MFPropertyDef]
        public MFIdentifier Finance_FamilyContribution = "MFiles.Property.FamilyContribution";
        [MFPropertyDef]
        public MFIdentifier Finance_FamilyContributionDiscount = "MFiles.Property.DiscountOnFamilyContribution";
    }
}
