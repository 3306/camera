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
            //while (true)
            //{
                
            //}
            
        }

        private  void server_DatagramReceived(object sender, TcpDatagramReceivedEventArgs<byte[]> e)
        {


         //   MessageBox.Show(e.Datagram[0].ToString());
             
           
        }
        private  void server_ClientDisconnected(object sender, TcpClientDisconnectEventArgs e)
        {
           
            MessageBox.Show("已经下线");
            string a = e.TcpClient.Client.RemoteEndPoint.ToString();
            a = a.Substring(0, a.LastIndexOf(":"));
            ThreadWithState q = new ThreadWithState(a);
            Thread t = new Thread(new ThreadStart(q.handleclientdisconnected));
            t.Start();
          
        }

        public  void server_ClientConnected(object sender, TcpClientConnectedEventArgs e)
        {
            //Thread a = new Thread(new ThreadStart(updateclient));
           // a.Start();
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

        private void button2_Click(object sender, EventArgs e)
        {
            //server.printclient();
            this.panel1.Controls.Clear();
            server.BeginMonitorClientConnected();
            updateclient();
           
        }


        private void check_Image(string  a)
        {
            //server.printclient();
            MessageBox.Show("我  被 点击");
            server.clients.Clear();
            MessageBox.Show(a);
            server.clients.Clear();
            server.IP_send_Image=a;
          
        }


        private void updateclient()
        {
            itero = 0;
                if (server.clients1.Keys == null)
                {
                    return;
                }
                foreach (var client in server.clients1)
                {
                    
                    itero +=100;
                    if (this.panel1.InvokeRequired)
                    {
                        updateclientcallback d = new updateclientcallback(updateclient);
                        this.Invoke(d, new object[] { });
                    }
                    else
                    {
                        Button a = new Button();
                        string aa = client.Key.ToString() + "点的人数是" + client.Value.Buffer[0];
                        a.Text = aa;
                        a.Click += delegate(Object o, EventArgs e) { check_Image(aa); };
                        
                   //     a.Click += new EventHandler<TcpClientConnectedEventArgs>(check_Image);
                        a.Width = 300;
                        a.Name = client.ToString();
                        a.Location = new  System.Drawing.Point(btn_x+itero, btn_y);
                        this.panel1.Controls.Add(a);
                    }
                    
               
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

      
     
        
    }
}
