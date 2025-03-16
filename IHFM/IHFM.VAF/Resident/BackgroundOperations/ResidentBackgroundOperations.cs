using IHFM.VAF.Import.Classes;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace IHFM.VAF
{
    public class ResidentBackgroundOperations
    {
        public void ImportResidentFormData(Vault vault, Configuration configuration)
        {
            try
            {
                var filePath = "C:\\Users\\singh\\source\\repos\\SandBlox\\MFilesThangs\\XMLFiles\\ffexport-20240515071029-1408745787.xml";

                XDocument doc = XDocument.Load(filePath);

                ResidentEnquiryForm residentEnquiryForm = new ResidentEnquiryForm(doc);

                bool devTestSingleRecord = true;

                


                //return;

                foreach (var site in residentEnquiryForm.Sites)
                {
                    Logger("=======================   SITE (" + site + ") START   =======================");
                    try
                    {
                        if (devTestSingleRecord)
                        {
                            Logger("PROCESSING SITE");
                            SiteSearchService siteSearchService = new SiteSearchService(vault, configuration);
                            var dsdss = siteSearchService.GetSiteByName(site);

                            if (dsdss != null)
                            {
                                Logger(dsdss.ObjID.ID + " -------> " + dsdss.Title);

                                //DO LOOKUPS FIRST SINCE THEY ACT AS VALIDATION SHORT CIRCUTS FOR IMPORT CURRENTLY

                                //EnquieryType TODO: NO MAPPING FOUND???????? ALSO NEED TO CHANGE TO MULTI SELECT LOOKUP
                                int EnquiryTypeLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_EnquiryTypeValueList, "Care Centre");

                                //LeadSource
                                int LeadSourceLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_LeadSourceValueList, "OtHEr");

                                //AccommodationUrgency TODO: CHANGE TO LOOKUP
                                //int AccommodationUrgencyLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_AccommodationUrgencyValueList, residentEnquiryForm.AccommodationUrgency.FirstOrDefault());
                                int AccommodationUrgencyLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_AccommodationUrgencyValueList, "Immediate");
                                //int AccommodationUrgencyLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_AccommodationUrgencyValueList, "Immediate "); //TODO: WhiteSpace testing and handling

                                //AccomodationRequired
                                int AccomodationRequiredLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_AccomodationRequiredValueList, residentEnquiryForm.AccommodationRequired.FirstOrDefault());

                                //GenderTitle
                                int GenderLookupId = PranWaySearchSingleSelect(vault, configuration.Resident_GendersValueList, residentEnquiryForm.Title);

                                //POTENTIALLY VALIDATIONS FOR TYPES

                                //ApplicationDate
                                //DateTime ApplicationDate = residentEnquiryForm.ApplicationDate;

                                //CellPhoneNumber
                                //var CellPhoneNumber = residentEnquiryForm.ContactNumber;


                                MFPropertyValuesBuilder propertyValuesBuilder = new MFPropertyValuesBuilder(vault);

                                propertyValuesBuilder.SetClass(configuration.ResidentClass);

                                propertyValuesBuilder.Add(configuration.Resident_ResidentDetail, MFDataType.MFDatatypeText, residentEnquiryForm.EmailAddress);
                                propertyValuesBuilder.Add(configuration.Resident_Site, MFDataType.MFDatatypeLookup, dsdss.ObjID.ID);
                                propertyValuesBuilder.Add(configuration.Resident_Surname, MFDataType.MFDatatypeText, residentEnquiryForm.Surname);
                                propertyValuesBuilder.Add(configuration.Resident_FirstName, MFDataType.MFDatatypeText, residentEnquiryForm.Name);
                                propertyValuesBuilder.Add(configuration.Resident_GenderTitle, MFDataType.MFDatatypeLookup, GenderLookupId);

                                //propertyValuesBuilder.SetLookup()
                                propertyValuesBuilder.AddLookup(configuration.Resident_EnquiryType, configuration.Resident_EnquiryTypeValueListItem_CareCentre);
                                propertyValuesBuilder.AddLookup(configuration.Resident_EnquiryType, configuration.Resident_EnquiryTypeValueListItem_IndependentLiving);

                                //propertyValuesBuilder.Add(configuration.Resident_EnquiryType, MFDataType.MFDatatypeMultiSelectLookup, EnquiryTypeLookupId);
                                //propertyValuesBuilder.Add(configuration.Resident_LeadSource, MFDataType.MFDatatypeMultiSelectLookup, LeadSourceLookupId);
                                //propertyValuesBuilder.Add(configuration.Resident_AccommodationUrgency, MFDataType.MFDatatypeLookup, AccommodationUrgencyLookupId);
                                //propertyValuesBuilder.Add(configuration.Resident_AccomodationRequired, MFDataType.MFDatatypeLookup, AccomodationRequiredLookupId);
                                propertyValuesBuilder.Add(configuration.Resident_ApplicationDate, MFDataType.MFDatatypeDate, residentEnquiryForm.ApplicationDate);
                                propertyValuesBuilder.Add(configuration.Resident_CellPhoneNumber, MFDataType.MFDatatypeText, residentEnquiryForm.ContactNumber);

                                var productionObjectCreation = vault.ObjectOperations.CreateNewObjectEx(configuration.ResidentObject.ID, propertyValuesBuilder.Values);

                                devTestSingleRecord = false;

                            }
                            else
                            {
                                //var jrei = siteSearchService.GetSiteByName("ds sdaas das dasdas asda");
                                //var fdds = jrei.ID; 
                            }

                            

                            //devTestSingleRecord = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger(ex.Message);
                    }
                    Logger("=======================   SITE (" + site + ") END   =======================" + Environment.NewLine + Environment.NewLine);
                }
            }
            catch (System.Exception ex)
            {
                
                var sdsd = ex.Message;
            }
        }

        private void Logger(string message)
        {
            File.AppendAllText("C:\\Vulpixel\\1_LenRenda\\Repo\\ImportResidentFormDataLogger.txt", DateTime.Now + ": " + message + Environment.NewLine);           
        }

        private void testValueListSearch(Vault _vault, MFIdentifier valueListIdentifier, string itemName)
        {

            MFSearchBuilder searchBuilder = new MFSearchBuilder(_vault);
            searchBuilder.ObjType(valueListIdentifier);
            searchBuilder.Deleted(false);
            searchBuilder.Property((int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle, MFDataType.MFDatatypeText, itemName);

            var sdsddd =  searchBuilder.FindOneEx();





            var conditions = new SearchConditions();

            var condition = new SearchCondition();
            condition.Expression.SetValueListItemExpression(MFValueListItemPropertyDef.MFValueListItemPropertyDefName, MFParentChildBehavior.MFParentChildBehaviorNone);
            condition.ConditionType = MFConditionType.MFConditionTypeContains;
            condition.TypedValue.SetValue(MFDataType.MFDatatypeText, itemName);
            conditions.Add(-1, condition);

            var testResults = _vault.ValueListItemOperations.SearchForValueListItemsEx(valueListIdentifier.ID, conditions);


        }

        private int PranWaySearchSingleSelect(Vault _vault, MFIdentifier valueListIdentifier, string searchText)
        {
            var valueListId = _vault.ValueListOperations.GetValueListIDByAlias(valueListIdentifier.Alias);
            ValueListItems valueListItems = _vault.ValueListItemOperations.GetValueListItems(valueListId);

            var enquiryValueListId = -1;

            foreach (ValueListItem item in valueListItems)
            {
                var sdsd = item.Name;
                //if (item.Name.ToLower().Contains(searchText.ToLower()))
                if (item.Name.ToLower() == searchText.ToLower())
                {
                    enquiryValueListId = item.ID;
                }
            }

            if (enquiryValueListId < 0)
            {
                throw new Exception("Value " + searchText + " not found in " + valueListIdentifier.Alias + " ValueList");
            }

            return enquiryValueListId;
        }

        private List<int> PranWaySearchMultiSelect(Vault _vault, MFIdentifier valueListIdentifier, List<string> searchItemsText)
        {
            List<int> valueListIds = new List<int>();

            foreach (var searchItemText in searchItemsText)
            {
                var valueListId = PranWaySearchSingleSelect(_vault, valueListIdentifier, searchItemText);

                if (valueListId > 0)
                    valueListIds.Add(valueListId);

            }

            return valueListIds;
        }

    }
}
