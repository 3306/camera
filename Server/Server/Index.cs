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
using System.Collections.Concurrent;
using System.Net;
using ServerDLL;
namespace Server
{
    public partial class Index : Form
    {
        

        static AsyncTcpServer server;
        delegate void updateclientcallback();
        delegate void handleclientdisconnectedcallback();
        int btn_x = 0;
        int btn_y = 0;
        int itero = 0;
        public Index()
        {
            InitializeComponent();
        }
        public void StartTheServer()
        {
            server = new AsyncTcpServer(8888);
            server.Encoding = Encoding.UTF8;
            server.ClientConnected += new EventHandler<TcpClientConnectedEventArgs>(server_ClientConnected);
            server.ClientDisconnected += new EventHandler<TcpClientDisconnectEventArgs>(server_ClientDisconnected);
            server.DatagramReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(server_DatagramReceived);
            server.Start();
            Print("服务器已启动");
        }
        /// <summary>
        /// 接收数据时引发的事件回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {    
           
        }
        /// <summary>
        /// 用户断开连接时引发的事件回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void server_ClientDisconnected(object sender, TcpClientDisconnectEventArgs e)
        {
           /*
            MessageBox.Show("已经下线");
            string a = e.TcpClient.Client.RemoteEndPoint.ToString();
            a = a.Substring(0, a.LastIndexOf(":"));
            ThreadWithState q = new ThreadWithState(a);
            Thread t = new Thread(new ThreadStart(q.handleclientdisconnected));
            t.Start();
            */
        }
        /// <summary>
        /// 有用户连接时引发的事件回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public  void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            Thread a = new Thread(new ThreadStart(updateclient));
            a.Start();
            //MessageBox.Show("已经上线" + ClientCount);
            
        }
        /// <summary>
        /// 启动监听的按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            
         
           
        }
      

        public void Print(string str)
        {
            this.Console_rbx.AppendText(str+"\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //server.printclient();
            this.panel1.Controls.Clear();
            server.BeginMonitorClientConnected();
            updateclient();
           
        }


        private void check_Image(string  ip,TcpClientImageState tcpClientState)
        {
            OperateCamera cameraform = new OperateCamera(tcpClientState, server);
            cameraform.Show();
            //server.printclient();        
           // server.clients.Clear();
            server.IP_send_Image=ip.ToString();
            //string optionsstr = "";

           // byte[] tag = new byte[8];
           // tag[0]=1;
            //TcpClient tcpclient = new TcpClient();
            //IPEndPoint ipe = new System.Net.IPEndPoint(IPAddress.Parse("192.168.1.107"), 9999);
            //tcpclient.Connect(ipe);
            //server.Send(tcpclient, tag);
        }


        private void updateclient()
        {
            itero = 0;
            ClassroomService classroomservice = new ClassroomService();
                if (server.clients.Keys == null)
                {
                    return;
                }
                foreach (var client in server.clients)
                {
                    
                    
                    if (this.panel1.InvokeRequired)
                    {
                        updateclientcallback d = new updateclientcallback(updateclient);
                        this.Invoke(d, new object[] { });
                    }
                    else
                    {
                        Button a = new Button();    
                        string aa = client.Key.ToString();
                        string currentclassroom = classroomservice.GetClassroom(aa);
                        a.Text = currentclassroom;
                        a.Click += delegate(Object o, EventArgs e) { check_Image(client.Key,client.Value); };         
                        a.Width = 100;
                        a.Name = client.ToString();
                        a.Location = new  System.Drawing.Point(btn_x+itero, btn_y);
                        this.panel1.Controls.Add(a);
                        
                    }
                    itero += 100;      
            }

        }

        public class ThreadWithState {
            public string ipaddress;
            public ThreadWithState(string ipaddress)
            {
                this.ipaddress = ipaddress;
            }
            public void handleclientdisconnected()
            {
                Index index = new Index();
                foreach (var a in index.Controls )
                {
                    if (a is System.Windows.Forms.Button)
                    {
                        Button btn = (Button)a;
                        if (btn.InvokeRequired)
                        {
                            handleclientdisconnectedcallback d = new handleclientdisconnectedcallback(handleclientdisconnected);
                            index.Invoke(d, new object[] { });
                        }
                        else
                        {
                            if (btn.Name == ipaddress)
                            {
                                btn.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Index_Load(object sender, EventArgs e)
        {
            StartTheServer();
            this.button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AddLessons a = new AddLessons();
            a.Show();
        }

    }
}
