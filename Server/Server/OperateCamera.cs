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
using System.Threading;
using System.Collections;

namespace Server
{
    public partial class OperateCamera : Form
    {
        TcpClientImageState tcpClientState;
        AsyncTcpServer server;
        private static string uid;
        delegate void GetImageCallBack();
        private static string CurrentIP;
        public OperateCamera(TcpClientImageState  tcpClientState,AsyncTcpServer server)
        {
            InitializeComponent();
            this.tcpClientState = tcpClientState;
            this.server = server;
        }

        private void GetImage_Click(object sender, EventArgs e)
        {
            
            string operatingstr = "getImage";
            byte[] operatingbyte = System.Text.Encoding.Default.GetBytes(operatingstr);
            TcpClient tcpclient = new TcpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(CurrentIP), 9999);
            tcpclient.Connect(ip);
            server.Send(tcpclient, operatingbyte);
            server.IP_send_Image = CurrentIP;
            //this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\2014-1-1 0-33-51.jpg");
            
            server.ImageReceived += server_ImageReceived;
        }

        void server_ImageReceived(object sender, TcpImageReceivedEventArgs<string> e)
        {
            uid = e.uid;
            //Thread a = new Thread(new ThreadStart(getimage));
            //a.Start();
            tcpClientState.TcpClient.Close();
            this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\" + uid + ".jpg");
        }

        private void SetCameraSpeed_Click(object sender, EventArgs e)
        {
            string cameraoptionsstr = this.comboBox1.SelectedValue.ToString();
            byte[] cameraoptionsbyte = System.Text.Encoding.Default.GetBytes(cameraoptionsstr);
            TcpClient tcpclient = new TcpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(CurrentIP), 9999);
            tcpclient.Connect(ip);
            server.Send(tcpclient, cameraoptionsbyte);
        }

        private void OperateCamera_Load(object sender, EventArgs e)
        {
            CurrentIP = tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().Substring(0, tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().LastIndexOf(":"));
            //初始化combobox
            ArrayList lst = new ArrayList();   　
            lst.Add(new Vendor("1", "1000"));  
            lst.Add(new Vendor("2", "2000"));
            lst.Add(new Vendor("3", "3000"));
            lst.Add(new Vendor("4", "4000"));
            lst.Add(new Vendor("5", "5000"));
            lst.Add(new Vendor("6", "6000"));
            lst.Add(new Vendor("7", "7000"));
            lst.Add(new Vendor("8", "8000"));
            comboBox1.Items.Clear();   
            comboBox1.DataSource = lst;           
            comboBox1.DisplayMember = "Strtemname";
            comboBox1.ValueMember = "Strindex";
            this.Text = CurrentIP+"控制台";
        }

        /*
        private void getimage()
        {
            string CurrentIP = tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().Substring(0, tcpClientState.TcpClient.Client.RemoteEndPoint.ToString().LastIndexOf(":"));
            if (this.CurrentImage.InvokeRequired)
            {
                GetImageCallBack d = new GetImageCallBack(getimage);
                this.Invoke(d,new object[] {});
            }
            else
            {
                this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\" + uid + ".jpg");
            }
        }
         */
        
        /// <summary>
        /// 退出摄像头操作窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToIndex_Click(object sender, EventArgs e)
        {
            /*
            string operatingstr = "getNum";
            byte[] operatingbyte = System.Text.Encoding.Default.GetBytes(operatingstr);
            TcpClient tcpclient = new TcpClient();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(CurrentIP), 9999);
            tcpclient.Connect(ip);
            server.Send(tcpclient, operatingbyte);
            server.IP_send_Image = null;
             */
            this.Close();

        }
        /// <summary>
        /// combobox用到的类
        /// </summary>
        public class Vendor
        {
            private string strtemname;
            private string strindex;
            public Vendor(string itemname, string index)
            {
                this.strtemname = itemname;
                this.strindex = index;
            }

            public string Strtemname
            {
                get { return strtemname; }
                set { strtemname = value; }
            }

            public string Strindex
            {
                get { return strindex; }
                set { strindex = value; }
            }
        }
    }
}
