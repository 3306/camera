using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ServerDLL;
using ListenerDLL;
using System.Drawing.Imaging;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.tSave.Enabled = false;
            Application.Exit();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                VC.Initialize(this.pictureBoxShow, this.pictureBoxShow.Width, this.pictureBoxShow.Height);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tSave_Tick(object sender, EventArgs e)
        {
            Save();
        }
        private void Save()
        {
            VC.CopyToClipBorad();
            Save_send_countPeople.Save(VC.getCaptureImage(), ControlCommand);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           
            //Save();
            this.tSave.Enabled = true;
        }

        private void btnBeginListen_Click(object sender, EventArgs e)
        {
            listener = new Listener();
            if (!listener.beginListen())
            {
                MessageBox.Show("监听失败");
            }
            listener.ControlReceived += new EventHandler<TcpDatagramReceivedEventArgs<byte[]>>(listener_ControReceive);

        }

        private void listener_ControReceive(object sender, TcpDatagramReceivedEventArgs<byte[]> e) {
            ControlCommand = e.Datagram;
      //      VC.CopyToClipBorad();
        //    Save_send_countPeople.Save(VC.getCaptureImage(), ControlCommand);
        }

        private void btnEndListen_Click(object sender, EventArgs e)
        {
            if (!Listener.endListen())
            {
                MessageBox.Show("关闭失败");
            }
            else
            {
                this.btnBeginListen.Enabled = true;
                this.btnEndListen.Enabled = false;
            }
        }

        private void frmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tSave.Enabled = false;
            Application.Exit();
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}