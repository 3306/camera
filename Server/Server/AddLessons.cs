using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ServerDLL;
namespace Server
{
    public partial class AddLessons : Form
    {
        Lessons lessonmodel = new Lessons();
        LessonsService lessonservice = new LessonsService();
        public AddLessons()
        {
            InitializeComponent();
        }

        private void AddLessons_Load(object sender, EventArgs e)
        {
            ArrayList weekdaylist = new ArrayList();
            ArrayList begintimelist = new ArrayList();
            ArrayList endtimelist = new ArrayList();
            weekdaylist.Add(new Vendor("Monday", "Monday"));
            weekdaylist.Add(new Vendor("Tuesday", "Tuesday"));
            weekdaylist.Add(new Vendor("Wednesday", "Wednesday"));
            weekdaylist.Add(new Vendor("Thursday", "Thursday"));
            weekdaylist.Add(new Vendor("Friday", "Friday"));
            for (int i = 1; i <= 11; i++)
            {
                begintimelist.Add(new Vendor("" + i + "", "" + i + ""));
                endtimelist.Add(new Vendor("" + i + "", "" + i + ""));
            }
            this.Weekday.DataSource = weekdaylist;
            this.Weekday.DisplayMember = "Strtemname";
            this.Weekday.ValueMember = "Strindex";
            this.BeginTime.DataSource = begintimelist;
            this.BeginTime.DisplayMember = "Strtemname";
            this.BeginTime.ValueMember = "Strindex";
            this.EndTime.DataSource = endtimelist;
            this.EndTime.DisplayMember = "Strtemname";
            this.EndTime.ValueMember = "Strindex";

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

        private void button1_Click(object sender, EventArgs e)
        {
            lessonmodel.lessonID = int.Parse(this.LessonID.Text);
            lessonmodel.lessonname = this.LessonName.Text;
            lessonmodel.weekday = this.Weekday.SelectedValue.ToString();
            lessonmodel.begintime = this.BeginTime.SelectedValue.ToString();
            lessonmodel.endtime = this.EndTime.SelectedValue.ToString();
            lessonmodel.numshouldbe = int.Parse(this.NumShouldBe.Text);
            int i = lessonservice.InsertLesson(lessonmodel);
            if (i == 1)
            {
                MessageBox.Show("添加成功");
                this.Close();
            }
            else
            {
                MessageBox.Show("添加失败");
            }
        }
    }
}
