using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace ServerDLL
{
    public class LessonsService
    {
        public int InsertLesson(Lessons LesModel)
        {
            int successNo = 0;
            DBHelper dbhelper = new DBHelper();
            SqlCommand cmd = new SqlCommand();
            string cmdstr = "insert Lessons (lessonID,lessonname,weekday,begintime,endtime,numshouldbe) values('" + LesModel.lessonID + "','" + LesModel.lessonname + "','" + LesModel.weekday + "','" + LesModel.begintime + "','" + LesModel.endtime + "','"+LesModel.numshouldbe+"')";
            cmd.Connection = dbhelper.connection;
            cmd.CommandText = cmdstr;
            try
            {
                cmd.Connection.Open();
                successNo = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
                
            }
            return successNo;
        }
        public Lessons GetLessonInfo(string weekday, string begintime, string endtime)
        {
            Lessons lesson = new Lessons();
            DBHelper dbhelp = new DBHelper();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbhelp.connection;

            string cmdstr= "select * from Lessons where weekday = '"+weekday+"' and  begintime='"+begintime+"' and endtime= '"+endtime+"'";
            cmd.CommandText = cmdstr;
            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lesson.lessonname = dr.GetString(5);
                    lesson.lessonID = dr.GetInt32(1);
                    lesson.numshouldbe = dr.GetInt32(6);
                }
                return lesson;
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (cmd.Connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        }
    }
}
