using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows.Forms;
namespace ListenerDLL
{
    public class Listener
    {
        //全局TcpClient
        static TcpClient client;
        //文件流建立到磁盘上的读写流    
        static FileStream fs;
        //buffer
        static int bufferlength = 200;
        static byte[] buffer = new byte[bufferlength];
        //网络流
        static NetworkStream ns;
        public static void ConnectAndListen()
        {
            //服务端监听任何IP 但是端口号是8888的连接
            TcpListener listener = new TcpListener(IPAddress.Any, 8888);
            //监听对象开始监听
            listener.Start();
            while (true)
            {
               
                Console.WriteLine("等待连接");
                //线程会挂在这里，直到客户端发来连接请求
                client = listener.AcceptTcpClient();
                Console.WriteLine("已经连接");
                //得到从客户端传来的网络流
                ns = client.GetStream();
                 Guid tempCartId = Guid.NewGuid();
                fs = new FileStream(Application.StartupPath + "\\" + tempCartId + ".jpg", FileMode.Create);
                //如果网络流中有数据
              if (ns.DataAvailable)
                {

                  
                    
                    /*同步读取网络流中的byte信息
                    do
                     {
                     ns.Read(buffer, 0, bufferlength);
                     } while (ns.Read(buffer, 0, bufferlength) > 0);
                    fs.Write(buffer, 0, bufferlength);
                    fs.Flush();
                     */
                    //异步读取网络流中的byte信息
                    ns.BeginRead(buffer, 0, bufferlength, ReadAsyncCallBack, null);
                }
            }
        }

        /// <summary>
        /// 异步读取
        /// </summary>
        /// <param name="result"></param>
        static void ReadAsyncCallBack(IAsyncResult result)
        {
            
            int readCount;
            //获得每次异步读取数量
            readCount = client.GetStream().EndRead(result);
            //如果全部读完退出，垃圾回收
            if (readCount < 1)
            {
                client.Close();
                ns.Dispose();
                fs.Dispose();
                return;
            }
            //将网络流中的图片数据片段顺序写入本地
            fs.Write(buffer, 0, bufferlength);
          
            //再次异步读取
            ns.BeginRead(buffer, 0, bufferlength, ReadAsyncCallBack, null);
        }

        













        /*
        private static int port;//新加的
        private static Socket sock;//新加的
        private static int number;//新加的大师大师
        private static int j;//新加的
        private static TcpListener listener;//新加的
        private static FileStream filestream;//新加的
        private static string fileStr = Application.StartupPath + "\\test.jpg";
        private static bool control = false;
        private static Thread thread;
        public static bool beginListen()
        {
            try
            {
                port = 8888;//默认端口号
                listener = new TcpListener(port);
                listener.Start();
                Thread thread = new Thread(new ThreadStart(recieve));
                thread.Start();     
                return true;
            }
            catch (Exception ee)
            {
                return false;
            }
            return false;

        }
        public static bool endListen()
        {
            try
            {
                if (listener != null)
                {
                    listener.Stop();
                    listener = null;

                    return true;
                }
            }
            catch
            {

            }
            return false;
        }
        private static void recieve()
        {
            //CheckForIllegalCrossThreadCalls
            //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//在线程中调用控件
            try
            {
                sock = listener.AcceptSocket();
                if (sock.Connected)
                {

                    byte[] bytee = System.Text.Encoding.BigEndianUnicode.GetBytes(fileStr.ToCharArray());
                    sock.Send(bytee, bytee.Length, 0);

                    //接受信息＋＋＋＋
                    while (!control)
                    {
                        NetworkStream stream = new NetworkStream(sock);
                        byte[] by = new Byte[1024];
                        int i = sock.Receive(by, by.Length, 0);

                        transfer(ref stream);
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }

        private static void transfer(ref NetworkStream stream)
        {
            try
            {
                filestream = new FileStream(fileStr, FileMode.Open, FileAccess.Read);
                //定义缓冲区
                byte[] bb = new byte[1024];
                //循环读文件

                while ((number = filestream.Read(bb, 0, 1024)) != 0)
                {//向客户端发送流
                    //sock.Send(bb,bb.Length,0);

                    stream.Write(bb, 0, 1024);
                    //刷新流
                    stream.Flush();

                    bb = new byte[1024];


                }
                bb = new byte[1024];
                bb = System.Text.Encoding.ASCII.GetBytes("<EOF>");
                sock.Send(bb);
                stream.Flush();
                filestream.Close();
                //sock.Close();
                stream.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {


            }


        }
         */
    }
}
