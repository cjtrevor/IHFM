using MFilesAPI;
using MFiles.VAF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCreateNewObjectFinalize, Class = "MFiles.Class.IncontinenceSupplies")]
        public void BeforeCreateNewIncontinenceSupplies(EventHandlerEnvironment env)
        {
            IncontinenceStockUpdateService updateService = new IncontinenceStockUpdateService(env.Vault, Configuration);

            int residentId = env.ObjVerEx.GetLookupID(Configuration.ResidentLookup);
            int productId = env.ObjVerEx.GetLookupID(Configuration.IncontinenceSupplies_IncontinenceProduct);
            int quantity = (int)env.ObjVerEx.GetProperty(Configuration.IncontinenceSupplies_Quantity).GetValue<double>();
            int packageSize =(int)(env.ObjVerEx.GetProperty(Configuration.IncontinenceSupplies_PackageSize).GetValue<double>());             
            //if(!Int32.TryParse(env.ObjVerEx.GetProperty(Configuration.IncontinenceSupplies_PackageSize).GetValueAsLocalizedText(), out packageSize))
            //{
            //    throw new Exception("The selected package size is not a valid number. Package sizes are not allowed to contain letters or decimals. Please select a valid package size and save again.");
            //}

            updateService.AdjustIncontinenceStockOnHand(residentId, productId, quantity * packageSize);

        }
    }
}
