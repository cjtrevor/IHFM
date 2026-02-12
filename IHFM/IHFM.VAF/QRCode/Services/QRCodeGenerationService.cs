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



            //var qrCodeData2 = qrGenerator.CreateQrCode("https://www.coingecko.com/en/coins/cardano", QRCodeGenerator.ECCLevel.Q);
            //var qrCodeData3 = qrGenerator.CreateQrCode("This is a test QR code", QRCodeGenerator.ECCLevel.Q);
            //var qrCodeData4 = qrGenerator.CreateQrCode("This is a test QR code", QRCodeGenerator.ECCLevel.M);

            //var qrCodeBytes = qrCodeData.GetRawData(QRCodeData.Compression.Uncompressed);

            //var testClass1 = new QRCodeHelper

            //PngByteQRCodeHelper.GetQRCode("https://www.coingecko.com/en/coins/cardano");

            //var pngQr = new PngByteQRCode(qrCodeData);
            //var byteThangs = pngQr.GetGraphic(25);

            //var pngQr2 = new PngByteQRCode(qrCodeData2);
            //var byteThangs2 = pngQr2.GetGraphic(25);

            //var pngQr3 = new PngByteQRCode(qrCodeData3);
            //var byteThangs3 = pngQr3.GetGraphic(25);

            //var pngQr4 = new PngByteQRCode(qrCodeData4);
            //var byteThangs4 = pngQr4.GetGraphic(25);


            //var nowTimePart = DateTime.Now.ToString("HH_mm_ss_");


            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\qrGen\\{nowTimePart}byteThangs.jpg", byteThangs);
            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\qrGen\\{nowTimePart}byteThangs2.png", byteThangs2);
            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\qrGen\\{nowTimePart}byteThangs3.png", byteThangs3);
            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\qrGen\\{nowTimePart}byteThangs4.png", byteThangs4);

            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\{nowTimePart}QRBytesJ.jpg", qrCodeBytes);
            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\{nowTimePart}QRBytesB.bmp", qrCodeBytes);
            //File.WriteAllBytes($"C:\\Vulpixel\\1_LenRenda\\Dump\\{nowTimePart}QRBytesG.gif", qrCodeBytes);



            //qrCodeData.SaveRawData($"C:\\Vulpixel\\1_LenRenda\\Dump\\{nowTimePart}QRCodeData.png", QRCodeData.Compression.Uncompressed);

            //return byteThangs;
            //throw new NotImplementedException("This method is a placeholder for the QR code generation logic. Please implement the necessary code to generate the QR code based on the requirements of your application.");
        }

    }
}
