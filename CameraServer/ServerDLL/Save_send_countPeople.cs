using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace ServerDLL
{
    public class Save_send_countPeople
    {
        private static write_pic_to_server write_pic_server=new write_pic_to_server();  
        private static ClassVedioCapture VC = new ClassVedioCapture();
        private static Face_detection face_detection=new Face_detection();
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
                //测试检测人头数
                string ImageURL = "C:\\2.jpg";
      //          MessageBox.Show(face_detection.HeadCounting(ImageURL).ToString());

                //检测图片中的人头数
                double  Head_sum = face_detection.HeadCounting(Application.StartupPath + "\\" + tempCartId + ".jpg");
         //       MessageBox.Show(Head_sum.ToString());
                FileStream fs = File.Open(Application.StartupPath + "\\" + tempCartId + ".jpg",FileMode.Open);
                byte[] fileBytes = new byte[fs.Length];
                using (fs)
                {
                    fs.Read(fileBytes,0,fileBytes.Length);
                    fs.Close();
                }
               

               bool Transmission_success = write_pic_server.Connection_write("192.168.1.100", "8888", fileBytes);
               if (Transmission_success)
               {
                 //  MessageBox.Show("传输图片成功");
               }
               else {
               //    MessageBox.Show("传输图片失败");
               
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
