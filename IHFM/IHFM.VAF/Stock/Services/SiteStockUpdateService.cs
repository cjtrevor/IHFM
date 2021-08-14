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

        public void UpdateSiteStock(int siteID, int stockID, int quantity)
        {
            ObjVerEx siteStockObjVer = FindSiteStock(siteID, stockID);

            if(siteStockObjVer == null)
            {
                CreateNewSiteStockObject(siteID,stockID,quantity);
                return;
            }

            UpdateStockOnHand(quantity, siteStockObjVer);
            siteStockObjVer.SaveProperties();
        }

        private void UpdateStockOnHand(int quantity, ObjVerEx siteStockObjVer)
        {
            int currentStock = (int)siteStockObjVer.GetProperty(_configuration.StockOnHand).TypedValue.Value;
            int updatedStock = currentStock + quantity;

            siteStockObjVer.SetProperty(_configuration.StockOnHand, MFDataType.MFDatatypeInteger, updatedStock);
        }
        private ObjVerEx FindSiteStock(int siteID, int stockID)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.Class(_configuration.SiteStock);
            mFSearchBuilder.Property(_configuration.Site, MFDataType.MFDatatypeLookup, siteID);
            mFSearchBuilder.Property(_configuration.Stock, MFDataType.MFDatatypeLookup, stockID);
            ObjectSearchResults objectSearchResults = mFSearchBuilder.Find();

            if (objectSearchResults.Count == 0)
                return null;
            else
                return mFSearchBuilder.FindOneEx();
        }
        private void CreateNewSiteStockObject(int siteID, int stockID, int quantity)
        {
            int siteStockObjectID = _vault.ObjectTypeOperations.GetObjectTypeIDByAlias(_configuration.SiteStockObject.Alias);
            PropertyValues propertyValues = new PropertyValues();
            
            PropertyValue nameProperty = new PropertyValue();
            nameProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle;
            nameProperty.TypedValue.SetValue(MFDataType.MFDatatypeText, $"Test site{siteID}-{stockID}");
            propertyValues.Add(0, nameProperty);

            PropertyValue classProperty = new PropertyValue();
            classProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass;
            classProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, _configuration.SiteStock.ID);
            propertyValues.Add(1, classProperty);

            PropertyValue siteProperty = new PropertyValue();
            siteProperty.PropertyDef = _configuration.Site.ID;
            siteProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, siteID);
            propertyValues.Add(2, siteProperty);

            PropertyValue stockProperty = new PropertyValue();
            stockProperty.PropertyDef = _configuration.Stock.ID;
            stockProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, stockID);
            propertyValues.Add(3, stockProperty);

            PropertyValue quantityProperty = new PropertyValue();
            quantityProperty.PropertyDef = _configuration.StockOnHand;
            quantityProperty.TypedValue.SetValue(MFDataType.MFDatatypeInteger, quantity);
            propertyValues.Add(4, quantityProperty);

            ObjectVersionAndProperties objectVersionAndProperties = _vault.ObjectOperations.CreateNewObject(siteStockObjectID, propertyValues);
            _vault.ObjectOperations.CheckIn(objectVersionAndProperties.ObjVer);
        }
    }
}
