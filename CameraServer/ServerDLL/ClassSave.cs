using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace ServerDLL
{
    public class ClassSave
    {
        private static write_pic_to_server write_pic_server=new write_pic_to_server();  
        private static ClassVedioCapture VC = new ClassVedioCapture();
        public static void Save(System.Drawing.Image i)
        {
            ImageCodecInfo ici;
            Encoder enc;
            EncoderParameter ep;
            EncoderParameters epa;

            try
            {
                //   Initialize   the   necessary   objects   
                ici = ClassEncode.GetEncoderInfo("image/jpeg");
                enc = Encoder.Quality;//设置保存质量                                                                         
                epa = new EncoderParameters(1);

                //   Set   the   compression   level   
                ep = new EncoderParameter(enc, 15L);//质量等级为25%   
                epa.Param[0] = ep;
                //i.Save(Application.StartupPath + "\\test.jpg", ici, epa);
                Guid tempCartId = Guid.NewGuid();

                i.Save(Application.StartupPath +"\\"+tempCartId+".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                if (!File.Exists(Application.StartupPath + "\\" + tempCartId + ".jpg")) return;
                FileStream fs = File.Open(Application.StartupPath + "\\" + tempCartId + ".jpg",FileMode.Open);
                byte[] fileBytes = new byte[fs.Length];
                using (fs)
                {
                    fs.Read(fileBytes,0,fileBytes.Length);
                    fs.Close();
                }
               

               bool Transmission_success = write_pic_server.Connection_write("127.0.0.1", "8888", fileBytes);
               if (Transmission_success)
               {
                   MessageBox.Show("传输图片成功");
               }
               else {
                   MessageBox.Show("传输图片失败");
               
               }

               i.Dispose();

             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
            
        }
        
    }
}
