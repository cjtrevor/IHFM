using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace IHFM.VAF
{
    public class DailyCareBackgroundOperations
    {
        public void GenerateProgressNotesPerResident(Vault vault, Configuration configuration)
        {
            if (IsValidPath(configuration.ProgressNotesExportPath))
            {
                if (!Directory.Exists(configuration.ProgressNotesExportPath))
                {
                    Directory.CreateDirectory(configuration.ProgressNotesExportPath);
                }
            }
            else
            {
                throw new Exception("Invalid or empty path: " + configuration.ProgressNotesExportPath);
            }

            List<int> residentsToProcess = new List<int>();

            SiteSearchService siteSearchService = new SiteSearchService(vault, configuration);
            ResidentSearchService residentSearchService = new ResidentSearchService(vault, configuration);

            List<ObjVerEx> sites = siteSearchService.GetAllSites();
            List<ObjVerEx> residents = residentSearchService.GetAllActiveResidents();

            List<int> includedZoneIds = new List<int> { configuration.Zone_FrailCareItem.ID, configuration.Zone_MemoryCareItem.ID };

            var exportPath = configuration.ProgressNotesExportPath;

            foreach (ObjVerEx site in sites)
            {
                int baseSiteID = site.ObjID.ID;

                ObjVerEx siteConfig = siteSearchService.GetSiteConfig(baseSiteID);

                if (siteConfig != null && siteConfig.HasValue(configuration.SiteConfig_GenerateAutoInterim) && siteConfig.GetProperty(configuration.SiteConfig_GenerateAutoInterim).GetValue<bool>())
                {
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
                            residentsToProcess.Add(resident.ObjID.ID);
                    }
                }
            }

            CreateXMLFile(residentsToProcess, configuration);
        }

        private void CreateXMLFile(List<int> residents, Configuration configuration)
        {
            if (residents != null && !residents.Any())
                return;

            var importElement = new XElement("Import");

            foreach (var item in residents)
            {
                var itemElement = new XElement("Item");

                itemElement.Add(new XElement("Class", configuration.DailyCare_ProgressNoteClass.ID));
                itemElement.Add(new XElement("Resident", item));
                itemElement.Add(new XElement("NoteType", configuration.DailyCare_InterimNoteType.ID));
                itemElement.Add(new XElement("CommentsNotes", "Add comments here"));
                itemElement.Add(new XElement("CreatedByCustom", 1));

                importElement.Add(itemElement);
            }

            if (!importElement.Elements("Item").Any())
                return;

            var doc = new XDocument(new XDeclaration("1.0", "utf-8", null), importElement);

            string filePath = Path.Combine(configuration.ProgressNotesExportPath, $"ProgressNotesExportPerResident_{DateTime.Now.ToString("yyyyMMdd_HH-mm-ss")}.xml");
            doc.Save(filePath);
        }

        private bool IsValidPath(string path)
        {
            try
            {
                var fullPath = Path.GetFullPath(path);

                char[] invalidChars = Path.GetInvalidPathChars();
                if (path.IndexOfAny(invalidChars) >= 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
