using MFiles.VAF.Common;
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
        [PropertyCustomValue("MFiles.Property.InvoiceTotal", Priority = 100)]
        public TypedValue SetFinanceInvoiceTotal(PropertyEnvironment env)
        {
            TypedValue value = new TypedValue();
            double tariff;
            double total;

            string tariffText = env.ObjVerEx.GetProperty(Configuration.Finance_Lodgings).GetValueAsLocalizedText();

            if(!Double.TryParse(tariffText, out tariff))
            {
                total = -1;
            }
            else
            {
                double familyContribution = env.ObjVerEx.HasValue(Configuration.Finance_FamilyContribution) ? env.ObjVerEx.GetProperty(Configuration.Finance_FamilyContribution).GetValue<double>() : 0;
                double familyContributionDiscount = env.ObjVerEx.HasValue(Configuration.Finance_FamilyContributionDiscount) ? env.ObjVerEx.GetProperty(Configuration.Finance_FamilyContributionDiscount).GetValue<double>() : 0;

                total = tariff + familyContribution - familyContributionDiscount;
            }

            value.SetValue(MFDataType.MFDatatypeFloating, total);

            return value;
        }
    }
}
