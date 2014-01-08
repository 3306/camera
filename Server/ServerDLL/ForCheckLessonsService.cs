using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace ServerDLL
{
    public class ForCheckLessonsService
    {
        public CheckLessons GetClassroomInfo(string classroomID)
        {
            DBHelper dbhelper = new DBHelper();
            SqlCommand cmd = new SqlCommand();
            string cmdstr = "select * from CheckLessons where classroomID="+classroomID+"";
            CheckLessons lessoninfo = new CheckLessons();
            cmd.Connection = dbhelper.connection;
            cmd.CommandText = cmdstr;
            try
            {
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lessoninfo.classroomID = classroomID;
                    lessoninfo.numshouldbe = dr.GetInt32(2);
                    lessoninfo.numrealbe = dr.GetInt32(3);

                }
            }
            catch (SqlException e)
            {

            }
            finally
            { 
            }
            return lessoninfo;
        }
        public int AddCheckLessonResult(CheckLessons checklesson)
        {
            DBHelper dbhelper = new DBHelper();
            SqlCommand cmd = new SqlCommand();
            string cmdstr = "insert CheckLessons (classroomID,numshouldbe,numrealbe,lessonname,checktime) values('" + checklesson.classroomID + "','" + checklesson.numshouldbe + "','" + checklesson.numrealbe + "','" + checklesson.lessonname + "','" + checklesson.checktime + "')";
            cmd.Connection = dbhelper.connection;
            cmd.CommandText = cmdstr;
            int successNo = 0;
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
