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
using ServerDLL;

namespace Server
{
    public partial class OperateCamera : Form
    {
        TcpClientImageState tcpClientState;
        AsyncTcpServer server;
        private static string uid;
        delegate void GetImageCallBack();
        private static string CurrentIP;
        delegate void showfacenumcallback();
        double facenum;
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
            //this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\2014-1-1 0-33-51.jpg");
            
            server.ImageReceived += server_ImageReceived;
        }

        void server_ImageReceived(object sender, TcpImageReceivedEventArgs<string> e)
        {
            uid = e.uid;
            string LessonBeginTime = "";
            string LessonEndTime = "";
            CheckLessons ChcLes = new CheckLessons();
            LessonsService LesService = new LessonsService();
            ClassroomService ClaService = new ClassroomService();
            ForCheckLessonsService CheckLessonsService = new ForCheckLessonsService();
            DateTime systime = DateTime.Now;
            int CurrentHour = systime.Hour;
            switch(CurrentHour)
            {
                case 8:
                    LessonBeginTime="1";
                    LessonEndTime="2";
                    break;
                case 9:
                    LessonBeginTime="2";
                    LessonEndTime="3";
                    break;
                case 10:
                    LessonBeginTime="3";
                    LessonEndTime="4";
                    break;
                case 11:
                    LessonBeginTime = "4";
                    LessonEndTime="5";
                    break;
                case 12:
                    LessonBeginTime="5";
                    LessonEndTime="6";
                    break;
                case 13:
                    LessonBeginTime="6";
                    LessonEndTime="7";
                    break;
                case 14:
                    LessonBeginTime="7";
                    LessonEndTime="8";
                    break;

            }
            DayOfWeek dayofweek = systime.DayOfWeek;
            Lessons CurrentLesson = new Lessons();
            tcpClientState.TcpClient.Close();
            try
            { 
                this.CurrentImage.Image = Image.FromFile(@System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\" + uid + ".jpg");
            }
            catch(FieldAccessException ex)
            {
                Console.Write(ex.ToString());
            }
            FaceDetection a = new FaceDetection();
            facenum= a.HeadCounting(System.Environment.CurrentDirectory + "\\pic\\" + CurrentIP + "\\" + uid + ".jpg");
            ChcLes.numrealbe = int.Parse(facenum.ToString());
            ChcLes.classroomID = ClaService.GetClassroom(CurrentIP);
            CurrentLesson = LesService.GetLessonInfo(dayofweek.ToString(),LessonBeginTime,LessonEndTime);
            ChcLes.numshouldbe = int.Parse(CurrentLesson.numshouldbe.ToString());
            ChcLes.lessonname = CurrentLesson.lessonname;
            ChcLes.checktime = systime;
            CheckLessonsService.AddCheckLessonResult(ChcLes);
            Thread b = new Thread(new ThreadStart(showfacenum));
            b.Start();
            

        }

        private void showfacenum()
        {
            if (this.facenum_label.InvokeRequired)
            {
                showfacenumcallback d = new showfacenumcallback(showfacenum);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.facenum_label.Text = facenum.ToString();
            }
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
            for (int i = 1; i < 8; i++)
            {
                lst.Add(new Vendor(""+i+"", ""+i+"000")); 
            }
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
            this.Dispose();
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
