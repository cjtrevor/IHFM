using IHFM.VAF.Import.Classes;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFilesAPI;
using System;
using System.IO;
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

                var sds = PranWaySearch(vault, configuration.Resident_AccomodationRequiredValueList, "Bachelor ground");

                var sds2 = PranWaySearch(vault, configuration.Resident_AccomodationRequiredValueList, "Bachelor groUnd flOor");

                //testValueListSearch(vault, configuration.Resident_AccomodationRequiredValueList, "Bachelor ground floor");

                //testValueListSearch(vault, configuration.Resident_AccomodationRequiredValueList, "Bachelor ground");

                return;

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
                                
                            }
                            else
                            {
                                //var jrei = siteSearchService.GetSiteByName("ds sdaas das dasdas asda");
                                //var fdds = jrei.ID; 
                            }

                            MFPropertyValuesBuilder propertyValuesBuilder = new MFPropertyValuesBuilder(vault);

                            devTestSingleRecord = false;
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

        private int PranWaySearch(Vault _vault, MFIdentifier valueListIdentifier, string searchText)
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

            return enquiryValueListId;
        }


    }
}
