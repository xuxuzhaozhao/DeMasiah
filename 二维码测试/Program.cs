using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ThoughtWorks.QRCode.Codec;

namespace 二维码测试
{
    class Program
    {
        static void Main(string[] args)
        {
            //http://www.cnblogs.com/jys509/p/4592539.html


            string nr = string.Empty;
            CreateCode_Simple("xuxuzhaozhao");
        }

        private static void CreateCode_Simple(string nr)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置加密模式，byte
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;

            System.Drawing.Image image = qrCodeEncoder.Encode(nr);
            string filename = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff").ToString()}.jpg";
            string filepath = $"D:\\{filename}";

            using (FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                image.Save(fs,ImageFormat.Jpeg);
                image.Dispose();
            } 
        }
    }
}
