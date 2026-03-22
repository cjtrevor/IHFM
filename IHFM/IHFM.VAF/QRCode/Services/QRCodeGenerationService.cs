using MFiles.VAF.Common;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO;

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
            var qrCodeBytes = pngQrCode.GetGraphic(25, drawQuietZones: false);

            return qrCodeBytes;
        }

        public byte[] GenerateFinalImage(byte[] qrCodeBytes, string displayText, int spacingAfterQRCode = 76, int textSize = 24, int qrPaddingSize = 110)
        {
            using (var qrStream = new MemoryStream(qrCodeBytes))
            using (var qrImage = System.Drawing.Image.FromStream(qrStream))
            {
                int spacing = spacingAfterQRCode;
                var font = new System.Drawing.Font("Arial", textSize);

                using (var tempBitmap = new System.Drawing.Bitmap(1, 1))
                using (var tempGraphics = System.Drawing.Graphics.FromImage(tempBitmap))
                {
                    var measuredTextSize = tempGraphics.MeasureString(displayText, font);

                    int qrWithPadding = qrImage.Width + (qrPaddingSize * 2);
                    int combinedWidth = Math.Max(qrWithPadding, (int)measuredTextSize.Width);
                    int combinedHeight = qrImage.Height + qrPaddingSize + spacing + (int)measuredTextSize.Height + 15;

                    var finalImage = new System.Drawing.Bitmap(combinedWidth, combinedHeight);
                    using (var graphics = System.Drawing.Graphics.FromImage(finalImage))
                    {
                        graphics.Clear(System.Drawing.Color.White);

                        int qrX = (combinedWidth - qrImage.Width) / 2;
                        graphics.DrawImage(qrImage, qrX, qrPaddingSize);

                        int textY = qrImage.Height + qrPaddingSize + spacing;
                        int textX = (combinedWidth - (int)measuredTextSize.Width) / 2;
                        graphics.DrawString(displayText, font, System.Drawing.Brushes.Black, textX, textY);
                    }

                    font.Dispose();
                    using (var ms = new MemoryStream())
                    {
                        finalImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return ms.ToArray();
                    }
                }
            }
        }

    }
}
