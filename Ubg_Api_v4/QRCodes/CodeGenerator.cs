using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;
using System.IO;


using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace Ubg_Api_v4.QRCodes
{
    //from Chronicus.pollsController
//    string path = Path.GetTempPath() + commitment.UserPollOptionId + ".ics";
//    myPaths.Add(path);
//                            if (System.IO.File.Exists(path))
//                            {
//                                System.IO.File.Delete(path);
//                            }
//FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
//StreamWriter str = new StreamWriter(fs);
//str.BaseStream.Seek(0, SeekOrigin.End);
//                            str.Write(icsFile);

//                            //Close Filestream and Streamwriter
//                            str.Flush();
//                            str.Dispose();
//                            fs.Dispose();

//                            Attachment atm = new Attachment(path);
//atm.Name = "Option (" + counter + ").ics";
//                            counter++;
//                            _messageService._msg.Attachments.Add(atm);
    public class CodeGenerator
    {
        QRCodeEncoder qrEncoder;
        String path = "C:\\Users\\david\\Dropbox\\";
        public CodeGenerator()
        {
            this.qrEncoder = new QRCodeEncoder();
        }


        public void RenderQrCode()
        {
            String saveIn = path + "Code1.jpg";
            Bitmap img = this.qrEncoder.Encode("ubg.transfer/347as9sd.com");
            img.Save(saveIn, ImageFormat.Jpeg);
            //img.Save("~/QRCodes/Codes/Code1.jpg", ImageFormat.Jpeg);
       

        }
        public Image RenderQRWithPicture()
        {
            String saveIn = path + "Code1.jpg";
            qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H; //30%
            qrEncoder.QRCodeScale = 10;


            Bitmap img = this.qrEncoder.Encode("ubg.transfer/347as9sd.com");

            System.Drawing.Image logo = System.Drawing.Image.FromFile(path + "ubgLogo.jpg");


            Graphics g = Graphics.FromImage(img);
            int left = (img.Width / 2) - (logo.Width / 2);
            int top = (img.Height / 2) - (logo.Height / 2);
            g.DrawImage(logo, new Point(left, top));


            img.Save(saveIn, ImageFormat.Jpeg);

            return img;


        }
    }
}