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
            ObjVerEx stockOnHand = FindResidentStockOnHand(residentId, productId);

            if(stockOnHand == null)
            {
                CreateNewResidentStockOnHand(residentId, productId, quantity);
                return;
            }

            UpdateStockOnHand(stockOnHand, quantity);
        }

        private void UpdateStockOnHand(ObjVerEx residentStockOnHand, int quantity)
        {
            int currentStock = residentStockOnHand.HasValue(_configuration.IncontinenceSupplies_StockOnHand) ? residentStockOnHand.GetProperty(_configuration.IncontinenceSupplies_StockOnHand).GetValue<int>() : 0;

            int updatedStock = currentStock + quantity;

            residentStockOnHand.SaveProperty(_configuration.IncontinenceSupplies_StockOnHand, MFDataType.MFDatatypeInteger, updatedStock);
        }

        private ObjVerEx FindResidentStockOnHand(int residentId, int productId)
        {
            MFSearchBuilder searchBuilder = new MFSearchBuilder(_vault);
            searchBuilder.Class(_configuration.IncontinenceSupplies_StockOnHandClass);
            searchBuilder.Property(_configuration.ResidentLookup,MFDataType.MFDatatypeLookup, residentId);
            searchBuilder.Property(_configuration.IncontinenceSupplies_IncontinenceProduct, MFDataType.MFDatatypeLookup, productId);

            ObjectSearchResults objectSearchResults = searchBuilder.Find();

            if (objectSearchResults.Count == 0)
                return null;
            else
                return searchBuilder.FindOneEx();
        }

        private void CreateNewResidentStockOnHand(int residentId, int productId, int quantity)
        {
            MFPropertyValuesBuilder propertyValuesBuilder = new MFPropertyValuesBuilder(_vault)
            .SetClass(_configuration.IncontinenceSupplies_StockOnHandClass)
            .Add(_configuration.ResidentLookup, MFDataType.MFDatatypeLookup, residentId)
            .Add(_configuration.IncontinenceSupplies_IncontinenceProduct, MFDataType.MFDatatypeLookup, productId)
            .Add(_configuration.IncontinenceSupplies_StockOnHand, MFDataType.MFDatatypeInteger, quantity);

            _vault.ObjectOperations.CreateNewObjectExQuick(_configuration.IncontinenceSupplies_Object.ID, propertyValuesBuilder.Values);
        }
    }
}
