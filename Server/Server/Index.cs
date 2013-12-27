using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyTcpServer;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    public partial class Index : Form
    {
        static AsyncTcpServer server;
        static FileStream fs;

        public Index()
        {
            InitializeComponent();
        }
        static void StartTheServer()
        {
            server = new AsyncTcpServer(8888);
            server.Encoding = Encoding.UTF8;
            server.ClientConnected += new EventHandler<TcpClientConnectedEventArgs>(server_ClientConnected);
            server.ClientDisconnected += new EventHandler<TcpClientDisconnectEventArgs>(server_ClientDisconnected);
            server.DatagramReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(server_DatagramReceived);
            server.Start();
            Console.WriteLine("TCP server has been started");
            Console.WriteLine("Type something to send to client");
            
            //while (true)
            //{
                
            //}
        }

        private static void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
            Guid id = Guid.NewGuid();
            fs = new FileStream(Application.StartupPath + "\\" + id + ".jpg", FileMode.Create);
            fs.Write(e.Datagram, 0, e.Datagram.Length);
           
        }
        private static void server_ClientDisconnected(object sender, TcpClientDisconnectEventArgs e)
        {
            MessageBox.Show(e.TcpClient.Client.RemoteEndPoint.ToString());
        }

        private static void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            
           // MessageBox.Show(e.TcpClient.Client.RemoteEndPoint.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartTheServer();
           
        }

        
    }
}
