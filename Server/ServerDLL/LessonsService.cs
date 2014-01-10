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
            string cmdstr = "insert Lessons (lessonID,lessonname,weekday,begintime,endtime) values('" + LesModel.lessonID + "','" + LesModel.lessonname + "','" + LesModel.weekday + "','" + LesModel.begintime + "','" + LesModel.endtime + "')";
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
    }
}
