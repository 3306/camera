using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyTcpServer;

namespace Server
{
    public partial class Index : Form
    {
        static AsyncTcpServer server; 
        public Index()
        {
            InitializeComponent();
        }
        static void StartTheServer()
        {
            server = new AsyncTcpServer(9999);
            server.Encoding = Encoding.UTF8;
            server.ClientConnected += new EventHandler<TcpClientConnectedEventArgs>(server_ClientConnected);
            server.ClientDisconnected += new EventHandler<TcpClientDisconnectEventArgs>(server_ClientDisconnected);
            server.PlaintextReceived += new EventHandler<TcpDatagramReceivedEventArgs<string>>(server_PlaintextReceived);
            server.Start();

            Console.WriteLine("TCP server has been started");
            Console.WriteLine("Type something to send to client");
            while (true)
            {
              
            }
        }

        private static void server_PlaintextReceived(object sender, TcpDatagramReceivedEventArgs<string> e)
        {
            MessageBox.Show("TCP client {0} has connected", e.TcpClient.Client.RemoteEndPoint.ToString());
        }

        private static void server_ClientDisconnected(object sender, TcpClientDisconnectEventArgs e)
        {
            MessageBox.Show("TCP client {0} has disconnected", e.TcpClient.Client.RemoteEndPoint.ToString());
        }

        private static void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            MessageBox.Show("TCP client {0} has connected", e.TcpClient.Client.RemoteEndPoint.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartTheServer();
        }
    }
}
