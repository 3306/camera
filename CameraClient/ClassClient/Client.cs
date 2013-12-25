using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace ClassClient
{
    public class Client
    {
        private static TcpClient client;
        private static NetworkStream netStream;
        private static FileStream filestream;
        public bool IsConnected = false;

        /// <summary>
        /// 
        /// </summary>
        private static void download()
        {
            try
            {
                down(ref netStream);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        private static void down(ref NetworkStream stream)
        {
            try
            {
                int length = 1024;
                byte[] bye = new byte[1024];
                int tt = stream.Read(bye, 0, length);
                //下行循环读取网络流并写进文件
                while (tt > 0)
                {
                    string ss = System.Text.Encoding.ASCII.GetString(bye);
                    int x = ss.IndexOf("<EOF>");
                    if (x != -1)
                    {
                        filestream.Write(bye, 0, x);
                        filestream.Flush();
                        break;
                    }
                    else
                    {
                        filestream.Write(bye, 0, tt);
                        filestream.Flush();
                    }
                    tt = stream.Read(bye, 0, length);
                }
                filestream.Close();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="sIP">前端窗口传入的服务器端IP</param>
        /// <param name="sProt">前端窗口传入的服务器端Port</param>
        /// <returns></returns>
        public bool beginConnection(string sIP, string sProt)
        {
            int port = 0;
            IPAddress myIP = IPAddress.Parse("127.0.0.1");
            client = new TcpClient();
            try
            {
                myIP = IPAddress.Parse(sIP);
            }
            catch
            {
                return false;
            }
            try
            {
                port = Int32.Parse(sProt);
            }
            catch
            {
                return false;
            }
            try
            {
                client.Connect(myIP, port);
                return true;
            }
            catch (Exception ee)
            {
                throw ee;
            }

        }
        public static bool getFile()
        {/*
            try
            {
                //构造新的文件流
                filestream = new FileStream(Application.StartupPath + "\\test.jpg", FileMode.OpenOrCreate, FileAccess.Write);
                //获取服务器网络流
                netStream = client.GetStream();
                byte[] by = System.Text.Encoding.BigEndianUnicode.GetBytes(downFile.ToCharArray());
                //向服务器发送要下载的文件名
                netStream.Write(by, 0, by.Length);
                //刷新流
                netStream.Flush();
                //启动接收文件的线程
                Thread thread = new Thread(new ThreadStart(download));
                thread.Start();
                return true;
            }
            catch (Exception)
            {


            }
            

            */
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool endConnection()
        {
            try
            {
                netStream = client.GetStream();
                string clo = "@@@@@@" + "\r\n";
                byte[] by = System.Text.Encoding.BigEndianUnicode.GetBytes(clo.ToCharArray());
                netStream.Write(by, 0, by.Length);
                netStream.Flush();
                client.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
