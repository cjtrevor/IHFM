using MFiles.VAF.Common;
using MFilesAPI;
using System;

namespace IHFM.VAF
{
    public class SiteStockUpdateService
    {
        private Vault _vault;
        private Configuration _configuration;
        public SiteStockUpdateService(Vault vault, Configuration configuration)
        {
            _vault = vault;
            _configuration = configuration;
        }

        public void UpdateSiteStock(int siteID, int stockID, double quantity, string itemName)
        {
            ObjVerEx siteStockObjVer = FindSiteStock(siteID, stockID);

            if(siteStockObjVer == null)
            {
                if (quantity < 0)
                    throw new Exception($"Insufficient stock of {itemName}. You cannot issue more stock than what is on hand. Current stock - 0");

                CreateNewSiteStockObject(siteID,stockID,quantity);
                return;
            }

            UpdateStockOnHand(quantity, siteStockObjVer,itemName);
            siteStockObjVer.SaveProperties();
        }

        private void UpdateStockOnHand(double quantity, ObjVerEx siteStockObjVer, string itemName)
        {

            double currentStock = siteStockObjVer.GetProperty(_configuration.StockOnHand).GetValue<double>();
            double updatedStock = currentStock + quantity;

            if (updatedStock < 0)
                throw new Exception($"Insufficient stock of {itemName}. You cannot issue more stock than what is on hand. Current stock - {currentStock}");

            siteStockObjVer.SetProperty(_configuration.StockOnHand, MFDataType.MFDatatypeFloating, updatedStock);
        }
        private ObjVerEx FindSiteStock(int siteID, int stockID)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.Class(_configuration.SiteStock);
            mFSearchBuilder.Property(_configuration.TranspharmStockSite, MFDataType.MFDatatypeLookup, siteID);
            mFSearchBuilder.Property(_configuration.TranspharmStock, MFDataType.MFDatatypeLookup, stockID);
            ObjectSearchResults objectSearchResults = mFSearchBuilder.Find();

            if (objectSearchResults.Count == 0)
                return null;
            else
                return mFSearchBuilder.FindOneEx();
        }
        private void CreateNewSiteStockObject(int siteID, int stockID, double quantity)
        {
            int siteStockObjectID = _vault.ObjectTypeOperations.GetObjectTypeIDByAlias(_configuration.SiteStockObject.Alias);
            PropertyValues propertyValues = new PropertyValues();
           
            PropertyValue classProperty = new PropertyValue();
            classProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass;
            classProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, _configuration.SiteStock.ID);
            propertyValues.Add(1, classProperty);

            PropertyValue siteProperty = new PropertyValue();
            siteProperty.PropertyDef = _configuration.TranspharmStockSite.ID;
            siteProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, siteID);
            propertyValues.Add(2, siteProperty);

            PropertyValue stockProperty = new PropertyValue();
            stockProperty.PropertyDef = _configuration.TranspharmStock.ID;
            stockProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, stockID);
            propertyValues.Add(3, stockProperty);

            PropertyValue quantityProperty = new PropertyValue();
            quantityProperty.PropertyDef = _configuration.StockOnHand;
            quantityProperty.TypedValue.SetValue(MFDataType.MFDatatypeFloating, quantity);
            propertyValues.Add(4, quantityProperty);

            ObjectVersionAndProperties objectVersionAndProperties = _vault.ObjectOperations.CreateNewObject(siteStockObjectID, propertyValues);
            _vault.ObjectOperations.CheckIn(objectVersionAndProperties.ObjVer);
        }
    }
}
