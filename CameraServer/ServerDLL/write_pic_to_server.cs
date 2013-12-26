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
        private static TcpClient client;
        private static NetworkStream netStream;
        private static FileStream filestream;
        private static int i;
        private static string downFile;


        public bool Connection_write(string sIP, string sProt, byte[] fileBytes)
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
                netStream = client.GetStream();
                using (netStream)
                {
                    netStream.Write(fileBytes, 0, fileBytes.Length);
                    netStream.Dispose();
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
