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
        String pathNotebook = "C:\\Users\\david\\Dropbox\\";
        String pathPC = "D:\\Dropbox\\";
        public CodeGenerator()
        {
            this.qrEncoder = new QRCodeEncoder();
        }


        public void RenderQrCode()
        {
            String saveIn = pathNotebook + "Code1.jpg";
            Bitmap img = this.qrEncoder.Encode("ubg.transfer/347as9sd.com");
            img.Save(saveIn, ImageFormat.Jpeg);
            //img.Save("~/QRCodes/Codes/Code1.jpg", ImageFormat.Jpeg);
       

        }
 
        public string RenderQRWithPicture(String url)
        {
            String saveIn = pathPC + "Code1.jpg";
            qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H; //30%
            qrEncoder.QRCodeScale = 10;


            Bitmap img = this.qrEncoder.Encode(url);

            System.Drawing.Image logo = System.Drawing.Image.FromFile(System.Web.Hosting.HostingEnvironment.MapPath(@"~\QRCodes\ubgLogo.jpg"));


            Graphics g = Graphics.FromImage(img);
            int left = (img.Width / 2) - (logo.Width / 2);
            int top = (img.Height / 2) - (logo.Height / 2);
            g.DrawImage(logo, new Point(left, top));


            string projectPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~\QRCodes\Code1.jpg");
            img.Save(projectPath, ImageFormat.Jpeg);
            //img.Save(saveIn, ImageFormat.Jpeg);

            
            string base64String = this.ImageToBase64(projectPath);
            return base64String;

        }

        public string ImageToBase64(String path )
        {
            string base64String = null;            
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}