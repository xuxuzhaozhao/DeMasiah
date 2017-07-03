using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace DeMasiah.Common
{
    public class QRCodeCommon
    {
        public static string CreateQRCode(string url)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeVersion = 5;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            Image image = qrCodeEncoder.Encode(url);
            var filename = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff").ToString()}.jpg";
            var filepath = HttpContext.Current.Server.MapPath("/QrCode");
            if (!Directory.Exists(filepath)) Directory.CreateDirectory(filepath);

            var file = $"{filepath}\\{filename}";

            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                image.Save(fs, ImageFormat.Jpeg);
                image.Dispose();
            }

            return $@"/QrCode/{filename}";
        }
    }
}