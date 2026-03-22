using IHFM.VAF.QRCode.Services;
using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.IO;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.QRCode")]
        public void BeforeQRCodeHourlyRounds_CheckinChangesFinalize(EventHandlerEnvironment env)
        {
            try
            {
                QRCodeGenerationService qrCodeGenerationService = new QRCodeGenerationService();

                var mfilesVaultGuid = Guid.Parse(env.Vault.GetGUID()).ToString("D");
                var classId = Configuration.QRCode_DailyCareObject_HourlyRoundsClass.ID;
                var residentLookupId = env.ObjVerEx.GetLookupID(Configuration.QRCode_Resident);

                Dictionary<int, string> properties = new Dictionary<int, string>
                {
                    { 100, classId.ToString() },
                    { Configuration.QRCode_Resident.ID, residentLookupId.ToString() },
                };

                var qrCodeImageBytes = qrCodeGenerationService.GenerateQRCodeImage(mfilesVaultGuid, Configuration.QRCode_Object.ID, properties);
                var qrCodeFinalImage = qrCodeGenerationService.GenerateFinalImage(qrCodeImageBytes, env.ObjVerEx.GetPropertyText(Configuration.QRCode_Resident));

                var objectId = env.ObjVerEx.ID;
                var saveFilePath = $"C:\\QRGenerationTempOutput\\{objectId}-{env.ObjVerEx.Version}.jpg";

                File.WriteAllBytes(saveFilePath, qrCodeFinalImage);
                env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
                env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"QRCode_HourlyRounds{objectId}-{env.ObjVerEx.Version}", "jpg", saveFilePath);
                File.Delete(saveFilePath);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
