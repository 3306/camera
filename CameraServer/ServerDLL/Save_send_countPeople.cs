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
        //设置保存图片路径
        private static string filePath = System.Environment.CurrentDirectory + "\\pic\\";
        public static void Save(System.Drawing.Image image,byte[] controlCommand)
        {

            string controlCommadstr = System.Text.Encoding.Default.GetString(controlCommand);
            ImageCodecInfo ici;
            Encoder enc;
            EncoderParameter ep;
            EncoderParameters epa;
            string ServerIPAddress = "192.168.1.102";
            string ServerPort = "8888";
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
                filePath = filePath + Path.DirectorySeparatorChar;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                image.Save(filePath + tempCartId + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                if (!File.Exists(filePath+ tempCartId + ".jpg")) return;
                //测试检测人头数
                
      //         MessageBox.Show(face_detection.HeadCounting(ImageURL).ToString());

                //检测图片中的人头数
                int   Head_sum = (int)face_detection.HeadCounting(filePath + tempCartId + ".jpg");
         //       MessageBox.Show(Head_sum.ToString());
                //发送的为分析后的图片中人头数量
                if (controlCommadstr.Equals("getNum"))
                {
                    byte[] fileBytes = new byte[8];
                    fileBytes[0] = (byte)Head_sum;
                    bool Transmission_success = write_pic_server.Connection_write(ServerIPAddress,ServerPort, fileBytes);

                    if (Transmission_success)
                    {
                        //  MessageBox.Show("传输图片成功");
                    }
                    else
                    {
                        //    MessageBox.Show("传输图片失败");

                    }
                }
                //发送的为图片字节流
                else if (controlCommadstr.Equals("getImage"))
                {
                    FileStream fs = File.Open(filePath + tempCartId + ".jpg", FileMode.Open);
                    byte[] fileBytes = new byte[fs.Length];
                    using (fs)
                    {
                        fs.Read(fileBytes, 0, fileBytes.Length);
                        fs.Close();
                    }
                    bool Transmission_success = write_pic_server.Connection_write(ServerIPAddress,ServerPort, fileBytes);

                    if (Transmission_success)
                    {
                        //  MessageBox.Show("传输图片成功");
                    }
                    else
                    {
                        //    MessageBox.Show("传输图片失败");

                    }
                }
                else { 
                
                }
                image.Dispose();

             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine(ex.Message);
            }
            
        }
        
    }
}
