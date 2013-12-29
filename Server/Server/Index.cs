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
using System.Threading;

namespace Server
{
    public partial class Index : Form
    {
        

        static AsyncTcpServer server;
      

        public Index()
        {
            InitializeComponent();
        }
        private int ClientCount = 0;
         private void StartTheServer()
        {
            server = new AsyncTcpServer(8888);
            server.Encoding = Encoding.UTF8;
            server.ClientConnected += new EventHandler<TcpClientConnectedEventArgs>(server_ClientConnected);
            server.ClientDisconnected += new EventHandler<TcpClientDisconnectEventArgs>(server_ClientDisconnected);
            server.DatagramReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(server_DatagramReceived);
            server.Start();
            ClientCount = 0;
            Print("服务器已启动");
            //while (true)
            //{
                
            //}
            
        }

        private  void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {
             
             
             
                
           
        }
        private  void server_ClientDisconnected(object sender, TcpClientDisconnectEventArgs e)
        {
            ClientCount--;
            MessageBox.Show("已经下线" + ClientCount);
          
        }

        private  void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            ClientCount++;
            //MessageBox.Show("已经上线" + ClientCount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartTheServer();
            this.button1.Enabled = false;
         
           
        }
        public void Print(string str)
        {
            this.Console_rbx.AppendText(str+"\n");
        }

        
    }
}
