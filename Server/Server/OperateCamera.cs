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
        AsyncTcpServer server;
        public OperateCamera(TcpClientDefaultState tcpClientState,AsyncTcpServer server)
        {
            InitializeComponent();
            this.tcpClientState = tcpClientState;
            this.server = server;
        }

        private void GetImage_Click(object sender, EventArgs e)
        {
            string CurrentIP = tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().Substring(0,tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().LastIndexOf(":"));
            string operatingstr = "getImage";
            byte[] operatingbyte = System.Text.Encoding.Default.GetBytes(operatingstr);
            TcpClient tcpclient = new TcpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(CurrentIP), 9999);
            tcpclient.Connect(ip);
            server.Send(tcpclient, operatingbyte);
            server.IP_send_Image = CurrentIP;
            this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\2014-1-1 0-33-51.jpg");
            //this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory+"\\pic\\"+CurrentIP+"\\"+server.NameOfImageFromClient+".jpg");
        }
        
    }
}
