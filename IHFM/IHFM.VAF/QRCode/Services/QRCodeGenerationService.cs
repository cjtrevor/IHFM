using MFiles.VAF.Common;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.QRCode.Services
{
    public class QRCodeGenerationService
    {
        public byte[] GenerateQRCodeImage(string vaultGuid, int objectId, Dictionary<int, string> properties)
        {
            string url = $"m-files://newobject/{vaultGuid}/{objectId}?";

            foreach (var prop in properties)
            {
                url += $"property={prop.Key}/{prop.Value}&";
            }

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            var pngQrCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = pngQrCode.GetGraphic(25);

            return qrCodeBytes;
        }
    }
}
