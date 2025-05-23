using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
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

        public void UpdateSiteStock(int siteID, int stockID, double quantity, string itemName, int stockTypeId)
        {
            ObjVerEx siteStockObjVer = FindSiteStock(siteID, stockID);

            if (quantity > 0)
            {
                //Transfer In so calculate the individual doses to be added.
                quantity = GetConvertedQuantity(stockID, quantity);
            }

            if (siteStockObjVer == null)
            {
                if (quantity < 0)
                    throw new Exception($"Insufficient stock of {itemName}. You cannot issue more stock than what is on hand. Current stock - 0");

                CreateNewSiteStockObject(siteID,stockID,quantity, stockTypeId);
                return;
            }

            UpdateStockOnHand(quantity, siteStockObjVer,itemName, stockTypeId);
            siteStockObjVer.SaveProperties();
        }

        public double GetConvertedQuantity(int stockID, double quantity)
        {
            ObjVerEx objVerEx = new ObjVerEx(_vault, _configuration.TranspharmStockObject.ID, stockID, -1);
            string qty = objVerEx.GetPropertyText(_configuration.TranspharmStockIssueQty);

            double issuingQuantity = 0;

            if(!double.TryParse(qty,out issuingQuantity))
            {
                throw new Exception($"Invalid issue quantity on stock id - {stockID} | Issueing Quantity: {qty}");
            }

            double convertedQuantity = quantity / issuingQuantity;

            return convertedQuantity;
        }

        private void UpdateStockOnHand(double quantity, ObjVerEx siteStockObjVer, string itemName, int stockTypeId)
        {

            double currentStock = siteStockObjVer.GetProperty(_configuration.StockOnHand).GetValue<double>();

            double updatedStock = currentStock + quantity;

            if (updatedStock < 0)
                throw new Exception($"Insufficient stock of {itemName}. You cannot issue more stock than what is on hand. Current stock - {currentStock}");

            siteStockObjVer.SetProperty(_configuration.StockOnHand, MFDataType.MFDatatypeFloating, updatedStock);

            //int stockTypePropertyId = siteStockObjVer.GetLookupID(_configuration.Stock_StockType);

            //if (!siteStockObjVer.HasProperty(_configuration.Stock_StockType))
            //{
                //set property value for siteStockObjVer to stockTypePropertyId

                //PropertyValue stockTypeProperty = new PropertyValue();
                //stockTypeProperty.PropertyDef = _configuration.Stock_StockType.ID;
                //stockTypeProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, stockTypeId);
                //propertyValues.Add(5, stockTypeProperty);
            //}

            //set property value for siteStockObjVer to stockTypePropertyId


        }
        private ObjVerEx FindSiteStock(int siteID, int stockID)
        {
            MFSearchBuilder mFSearchBuilder = new MFSearchBuilder(_vault);
            mFSearchBuilder.Class(_configuration.SiteStock);
            mFSearchBuilder.Property(_configuration.VAFSite, MFDataType.MFDatatypeLookup, siteID);
            mFSearchBuilder.Property(_configuration.TranspharmStock, MFDataType.MFDatatypeLookup, stockID);
            ObjectSearchResults objectSearchResults = mFSearchBuilder.Find();

            if (objectSearchResults.Count == 0)
                return null;
            else
                return mFSearchBuilder.FindOneEx();
        }
        private void CreateNewSiteStockObject(int siteID, int stockID, double quantity, int stockTypeId)
        {
            int siteStockObjectID = _vault.ObjectTypeOperations.GetObjectTypeIDByAlias(_configuration.SiteStockObject.Alias);
            PropertyValues propertyValues = new PropertyValues();
           
            PropertyValue classProperty = new PropertyValue();
            classProperty.PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass;
            classProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, _configuration.SiteStock.ID);
            propertyValues.Add(1, classProperty);

            PropertyValue siteProperty = new PropertyValue();
            siteProperty.PropertyDef = _configuration.VAFSite.ID;
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

            PropertyValue stockTypeProperty = new PropertyValue();
            stockTypeProperty.PropertyDef = _configuration.Stock_StockType.ID;
            stockTypeProperty.TypedValue.SetValue(MFDataType.MFDatatypeLookup, stockTypeId);
            propertyValues.Add(5, stockTypeProperty);

            ObjectVersionAndProperties objectVersionAndProperties = _vault.ObjectOperations.CreateNewObject(siteStockObjectID, propertyValues);
            _vault.ObjectOperations.CheckIn(objectVersionAndProperties.ObjVer);
        }

        public void CreateNewStockIssue(int siteID, ObjVerEx issue)
        {
            SiteStockUpdateService siteStockUpdateService = new SiteStockUpdateService(_vault, _configuration);

            string transfer = issue.HasProperty(_configuration.Transfer) ? issue.GetPropertyText(_configuration.Transfer) : "out";
            int stockTypeId = issue.GetLookupID(_configuration.Stock_StockType);

            int item1StockID = issue.GetLookupID(_configuration.Item1Stock);
            if (item1StockID > -1)
            {
                string itemName = issue.GetPropertyText(_configuration.Item1Stock);
                double item1Quantity = issue.GetProperty(_configuration.Item1StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item1StockID, transfer.ToLower() == "in" ? item1Quantity : -item1Quantity, itemName, stockTypeId);
            }

            int item2StockID = issue.GetLookupID(_configuration.Item2Stock);
            if (item2StockID > -1)
            {
                string itemName = issue.GetPropertyText(_configuration.Item2Stock);
                double item2Quantity = issue.GetProperty(_configuration.Item2StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item2StockID, transfer.ToLower() == "in" ? item2Quantity : -item2Quantity, itemName, stockTypeId);
            }

            int item3StockID = issue.GetLookupID(_configuration.Item3Stock);
            if (item3StockID > -1)
            {
                string itemName = issue.GetPropertyText(_configuration.Item3Stock);
                double item3Quantity = issue.GetProperty(_configuration.Item3StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item3StockID, transfer.ToLower() == "in" ? item3Quantity : -item3Quantity, itemName, stockTypeId);
            }

            int item4StockID = issue.GetLookupID(_configuration.Item4Stock);
            if (item4StockID > -1)
            {
                string itemName = issue.GetPropertyText(_configuration.Item4Stock);
                double item4Quantity = issue.GetProperty(_configuration.Item4StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item4StockID, transfer.ToLower() == "in" ? item4Quantity : -item4Quantity, itemName, stockTypeId);
            }

            int item5StockID = issue.GetLookupID(_configuration.Item5Stock);
            if (item5StockID > -1)
            {
                string itemName = issue.GetPropertyText(_configuration.Item5Stock);
                double item5Quantity = issue.GetProperty(_configuration.Item5StockQuantityIssued).GetValue<double>();
                siteStockUpdateService.UpdateSiteStock(siteID, item5StockID, transfer.ToLower() == "in" ? item5Quantity : -item5Quantity, itemName, stockTypeId);
            }

            if (transfer.ToLower() == "in")
            {
                issue.RemoveProperty(_configuration.ResidentLookup);
                issue.SaveProperties();
            }
        }
    }
}
