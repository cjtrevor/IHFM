using MFiles.VAF.Common;
using MFilesAPI;
using System.Collections.Generic;
using System.Linq;

namespace IHFM.VAF
{
    public class DailyCareBackgroundOperations
    {
        public void GenerateProgressNotesPerResident(VaultApplication vaultApplication, Vault vault, Configuration configuration)
        {
            ResidentSearchService residentSearchService = new ResidentSearchService(vault, configuration);
            SiteSearchService siteSearchService = new SiteSearchService(vault, configuration);

            List<ObjVerEx> sites = siteSearchService.GetAllSites();
            List<ObjVerEx> residents = residentSearchService.GetAllActiveResidents();

            List<int> includedZoneIds = new List<int> { configuration.Zone_FrailCareItem.ID, configuration.Zone_MemoryCareItem.ID };

            //Do we really need to loop over sites? This might be here because of a specific implementation at some point
            //Technically, since permission seems to be linked to residents, we could just loop over residents
            foreach (ObjVerEx site in sites)
            {
                int baseSiteID = site.GetLookupID(configuration.BaseSiteID);
                List<ObjVerEx> siteResidents = residents.Where(x => x.GetLookupID(configuration.BaseSiteID) == baseSiteID).ToList();

                foreach (ObjVerEx resident in siteResidents)
                {
                    var currentRoomLookup = resident.GetProperty(configuration.CurrentRoom).TypedValue.GetValueAsLookup();
                    if (currentRoomLookup == null)
                        continue;

                    var roomObject = new ObjVerEx(vault, currentRoomLookup);
                    if (roomObject == null || roomObject.IsDeleted)
                        continue;

                    var currentRoomZoneId = roomObject.GetLookupID(configuration.Room_Zone);

                    if (includedZoneIds.Contains(currentRoomZoneId))
                    {
                        MFPropertyValuesBuilder propertyValuesBuilder = new MFPropertyValuesBuilder(vault);
                        propertyValuesBuilder.SetClass(configuration.DailyCare_ProgressNoteClass);

                        propertyValuesBuilder.Add(configuration.DailyCare_Resident, MFDataType.MFDatatypeLookup, resident.ObjID.ID);
                        propertyValuesBuilder.Add(configuration.DailyCare_NoteType, MFDataType.MFDatatypeLookup, configuration.DailyCare_InterimNoteType.ID);
                        propertyValuesBuilder.Add(configuration.DailyCare_CommentsNotes, MFDataType.MFDatatypeMultiLineText, "To be completed.");
                        propertyValuesBuilder.Add(configuration.DailyCare_CreatedBy, MFDataType.MFDatatypeLookup, 1);
                        propertyValuesBuilder.Add(MFBuiltInPropertyDef.MFBuiltInPropertyDefCreatedBy, MFDataType.MFDatatypeLookup, 73);
                        propertyValuesBuilder.Add(configuration.GCSRequired, MFDataType.MFDatatypeBoolean, false);

                        var newObj = vault.ObjectOperations.CreateNewObjectEx(configuration.DailyCareObject, propertyValuesBuilder.Values);

                        vaultApplication.AddItemToSequentialQueue(new EventHandlerEnvironment
                        {
                            Vault = vaultApplication.PermanentVault,
                            Input = newObj.ToString()
                        });
                    }
                }
            }
        }
    }
}
