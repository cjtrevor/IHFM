using IHFM.VAF.QRCode.Services;
using MFiles.VAF.Common;
using MFilesAPI;
using SSRS_Reporting.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace IHFM.VAF
{
    public partial class VaultApplication
    {
        //[EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.QRCode")]
        //public void BeforeQRCode_CheckinChangesFinalize(EventHandlerEnvironment env)
        //{
        //    return;
        //    try
        //    {
        //        QRCodeGenerationService qrCodeGenerationService = new QRCodeGenerationService();

        //        var residentLookupId = env.ObjVerEx.GetLookupID(Configuration.QRCode_Resident);
        //        var roomList = env.ObjVerEx.GetLookupID(Configuration.QRCode_Room_List);

        //        string mfilesVaultGuid = Guid.Parse(env.Vault.GetGUID()).ToString("D");
        //        var qrClassId = env.ObjVerEx.GetLookupID(Configuration.QRCode_QRClass);

        //        int classId = -1;
        //        switch (qrClassId)
        //        {
        //            case 1:
        //                classId = Configuration.QRCode_DailyCareObject_HourlyRoundsClass.ID;
        //                break;
        //        }

        //        Dictionary<int, string> properties = new Dictionary<int, string>
        //        {
        //            { 100, classId.ToString() },
        //            { Configuration.QRCode_Resident.ID, residentLookupId.ToString() },
        //            { Configuration.QRCode_Room_List.ID, roomList.ToString() }
        //        };

        //        var qrCodeImageBytes = qrCodeGenerationService.GenerateQRCodeImage(mfilesVaultGuid, Configuration.QRCode_DailyCareObject.ID, properties);

        //        var objectId = env.ObjVerEx.ID;
        //        File.WriteAllBytes($"C:\\SSRS Temp Output\\{objectId}.jpg", qrCodeImageBytes);
        //        env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
        //        env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"QRCode{objectId}-{env.ObjVerEx.Version}", "jpg", $"C:\\SSRS Temp Output\\{objectId}.jpg");
        //        File.Delete($"C:\\SSRS Temp Output\\{objectId}.jpg");

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, Class = "MFiles.Class.QrHourlyRounds")]
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

                var qrCodeImageBytes = qrCodeGenerationService.GenerateQRCodeImage(mfilesVaultGuid, Configuration.QRCode_DailyCareObject.ID, properties);

                var objectId = env.ObjVerEx.ID;
                File.WriteAllBytes($"C:\\QRGenerationTempOutput\\{objectId}.jpg", qrCodeImageBytes);
                env.Vault.ObjectFileOperations.GetFilesForModificationInEventHandler(env.ObjVer);
                env.Vault.ObjectFileOperations.AddFile(env.ObjVer, $"QRCode_HourlyRounds{objectId}-{env.ObjVerEx.Version}", "jpg", $"C:\\QRGenerationTempOutput\\{objectId}.jpg");
                File.Delete($"C:\\QRGenerationTempOutput\\{objectId}.jpg");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
