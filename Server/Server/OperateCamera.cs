using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AsyTcpServer;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class OperateCamera : Form
    {
        TcpClientDefaultState tcpClientState;
        static AsyncTcpServer server;
        public OperateCamera(TcpClientDefaultState tcpClientState)
        {
            InitializeComponent();
            this.tcpClientState = tcpClientState;
        }

        private void GetImage_Click(object sender, EventArgs e)
        {
            string CurrentIP = tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().Substring(0,tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().LastIndexOf(":"));
            string operatingstr = "GETIMAGE";
            byte[] operatingbyte = System.Text.Encoding.Default.GetBytes(operatingstr);
            TcpClient tcpclient = new TcpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(CurrentIP), 9999);
            tcpclient.Connect(ip);
            server.Send(tcpclient, operatingbyte);         
        }
        
    }
}
