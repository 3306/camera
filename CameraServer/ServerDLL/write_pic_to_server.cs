using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ServerDLL
{   
    class write_pic_to_server
    {
       private   static  IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
        private    static  string myip = IpEntry.AddressList[2].ToString();
        private static IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(myip), 7777);
      // private  static TcpClient client = new TcpClient(ipe);
       private static TcpClient client;
        
        private static NetworkStream netStream;
        private static FileStream filestream;
        private static int i;
        private static string downFile;


        public bool Connection_write(string sIP, string sProt, byte[] fileBytes)
        {


            int port = 0;
            IPAddress myIP = IPAddress.Parse("192.168.1.100");
            client = new TcpClient(ipe);
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

                netStream = client.GetStream();
                using (netStream)
                {
                    netStream.Write(fileBytes, 0, fileBytes.Length);
                    //netStream.Dispose();
                }
                return true;
            }
            catch (Exception ee)
            {
                throw ee;
            }

        }



    }
}
