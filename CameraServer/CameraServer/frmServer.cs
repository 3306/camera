using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ServerDLL;
using ListenerDLL;
using System.Drawing.Imaging;
using System.Timers;
using System.Threading;
namespace CameraServer
{
    public partial class frmServer : Form
    {
        private static  string str = "getImage";
       private  static    byte[] ControlCommand = System.Text.Encoding.Default.GetBytes(str);
        private   static ClassVedioCapture VC = new ClassVedioCapture();
        private static Listener listener;
        public frmServer()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            listener = new Listener();
            if (!listener.beginListen())
            {
                MessageBox.Show("监听失败");
            }
            listener.ControlReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(listener_ControReceive);
            try
            {
                VC.Initialize(this.pictureBoxShow, this.pictureBoxShow.Width, this.pictureBoxShow.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Save();
        }
        private  static void Save()
        {
            VC.CopyToClipBorad();
            Save_send_countPeople.Save(VC.getCaptureImage(), ControlCommand);
        }
    
        private void listener_ControReceive(object sender, TcpDatagramReceivedEventArgs<byte[]> e) {
            
            ControlCommand = e.Datagram;
            if (!System.Text.Encoding.Default.GetString(e.Datagram).Equals("getImage"))
            {  
                int   speed = int.Parse(System.Text.Encoding.Default.GetString(e.Datagram));
                System.Timers.Timer timer = new System.Timers.Timer(speed);
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Enabled = true;
            }
        }
        delegate void aa(string s);//创建一个代理
        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Thread newthread = new Thread(new ThreadStart(ttread));
            newthread.Start();
        }
        private void btnEndListen_Click(object sender, EventArgs e)
        {
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
         
        }

     
      private  void ttread()
        {
            pri("sdfs");
        }

      private  void pri(string t)//这个就是我们的函数，我们把要对控件进行的操作放在这里
      {
          //if(this.InvokeRequired)
          if (!richTextBox1.InvokeRequired)//判断是否需要进行唤醒的请求，如果控件与主线程在一个线程内，可以写成if(!InvokeRequired)
          {

             // MessageBox.Show("同一线程内");
              Save();
          }
          else
          {
           //   MessageBox.Show("不是同一个线程");
              aa a1 = new aa(pri);
              Invoke(a1, new object[] { t });//执行唤醒操作
          }
      }

      private void frmServer_Activated(object sender, EventArgs e)
      {
         // this.Hide();
      }

      private void richTextBox1_TextChanged(object sender, EventArgs e)
      {

      }

      
    }
}