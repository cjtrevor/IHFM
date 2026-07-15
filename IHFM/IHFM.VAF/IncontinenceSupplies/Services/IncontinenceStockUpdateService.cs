using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF
{
    public class IncontinenceStockUpdateService
    {
        private readonly Vault _vault;
        private readonly Configuration _configuration;

        public IncontinenceStockUpdateService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void AdjustIncontinenceStockOnHand(int residentId, int productId, int quantity)
        {
            DailyCareLogger.Log($"IncontinenceStockUpdateService.AdjustIncontinenceStockOnHand START — resident={residentId}, product={productId}, qty={quantity}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            DailyCareLogger.Log("IncontinenceStockUpdateService.AdjustIncontinenceStockOnHand — searching for existing stock record");
            ObjVerEx stockOnHand = FindResidentStockOnHand(residentId, productId);

            if(stockOnHand == null)
            {
                DailyCareLogger.Log("IncontinenceStockUpdateService.AdjustIncontinenceStockOnHand — no record, creating new");
                CreateNewResidentStockOnHand(residentId, productId, quantity);
                sw.Stop();
                DailyCareLogger.Log($"IncontinenceStockUpdateService.AdjustIncontinenceStockOnHand END — elapsed={sw.ElapsedMilliseconds}ms");
                return;
            }

            DailyCareLogger.Log("IncontinenceStockUpdateService.AdjustIncontinenceStockOnHand — updating stock");
            UpdateStockOnHand(stockOnHand, quantity);

            sw.Stop();
            DailyCareLogger.Log($"IncontinenceStockUpdateService.AdjustIncontinenceStockOnHand END — elapsed={sw.ElapsedMilliseconds}ms");
        }

        private void UpdateStockOnHand(ObjVerEx residentStockOnHand, int quantity)
        {
            DailyCareLogger.Log("IncontinenceStockUpdateService.UpdateStockOnHand START");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            int currentStock = residentStockOnHand.HasValue(_configuration.IncontinenceSupplies_StockOnHand) ? residentStockOnHand.GetProperty(_configuration.IncontinenceSupplies_StockOnHand).GetValue<int>() : 0;
            int updatedStock = currentStock + quantity;
            residentStockOnHand.SaveProperty(_configuration.IncontinenceSupplies_StockOnHand, MFDataType.MFDatatypeInteger, updatedStock);

            sw.Stop();
            DailyCareLogger.Log($"IncontinenceStockUpdateService.UpdateStockOnHand END — updatedStock={updatedStock}, elapsed={sw.ElapsedMilliseconds}ms");
        }

        private ObjVerEx FindResidentStockOnHand(int residentId, int productId)
        {
            DailyCareLogger.Log($"IncontinenceStockUpdateService.FindResidentStockOnHand START — resident={residentId}, product={productId}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            MFSearchBuilder searchBuilder = new MFSearchBuilder(_vault);
            searchBuilder.Class(_configuration.IncontinenceSupplies_StockOnHandClass);
            searchBuilder.Property(_configuration.ResidentLookup,MFDataType.MFDatatypeLookup, residentId);
            searchBuilder.Property(_configuration.IncontinenceSupplies_IncontinenceProduct, MFDataType.MFDatatypeLookup, productId);

            ObjectSearchResults objectSearchResults = searchBuilder.Find();

            sw.Stop();
            DailyCareLogger.Log($"IncontinenceStockUpdateService.FindResidentStockOnHand END — found={objectSearchResults.Count > 0}, elapsed={sw.ElapsedMilliseconds}ms");

            if (objectSearchResults.Count == 0)
                return null;
            else
                return searchBuilder.FindOneEx();
        }

        private void CreateNewResidentStockOnHand(int residentId, int productId, int quantity)
        {
            DailyCareLogger.Log($"IncontinenceStockUpdateService.CreateNewResidentStockOnHand START — resident={residentId}, product={productId}, qty={quantity}");
            var sw = System.Diagnostics.Stopwatch.StartNew();

            MFPropertyValuesBuilder propertyValuesBuilder = new MFPropertyValuesBuilder(_vault)
            .SetClass(_configuration.IncontinenceSupplies_StockOnHandClass)
            .Add(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId)
            .Add(_configuration.IncontinenceSupplies_IncontinenceProduct, MFDataType.MFDatatypeLookup, productId)
            .Add(_configuration.IncontinenceSupplies_StockOnHand, MFDataType.MFDatatypeInteger, quantity);
            _vault.ObjectOperations.CreateNewObjectExQuick(_configuration.IncontinenceSupplies_Object.ID, propertyValuesBuilder.Values);

            sw.Stop();
            DailyCareLogger.Log($"IncontinenceStockUpdateService.CreateNewResidentStockOnHand END — elapsed={sw.ElapsedMilliseconds}ms");
        }
    }
}
