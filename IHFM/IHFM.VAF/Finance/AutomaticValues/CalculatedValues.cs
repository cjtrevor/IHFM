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
            string totalText;

            string tariffText = env.ObjVerEx.GetProperty(Configuration.Finance_Lodgings).GetValueAsLocalizedText();

            if(!Double.TryParse(tariffText, out tariff))
            {
                totalText = "Unable to calculate room tariff.";
            }
            else
            {
                double familyContribution = env.ObjVerEx.GetProperty(Configuration.Finance_FamilyContribution).GetValue<double>();
                double familyContributionDiscount = env.ObjVerEx.GetProperty(Configuration.Finance_FamilyContributionDiscount).GetValue<double>();

                totalText = (tariff + familyContribution - familyContributionDiscount).ToString();
            }

            value.SetValue(MFDataType.MFDatatypeText, totalText);

            return value;
        }
    }
}
