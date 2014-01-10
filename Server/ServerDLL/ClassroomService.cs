using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace ServerDLL
{
    public class ClassroomService
    {
        public string GetClassroom(string cameraIP)
        {
            DBHelper dbhelper = new DBHelper();
            SqlDataReader dbread;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbhelper.connection;
            cmd.CommandText = "select * from Classroom where cameraIP =@cameraIP";
            cmd.Parameters.AddWithValue("@cameraIP", cameraIP);
            string currentclassroomID="";
            try
            {
                cmd.Connection.Open();
                dbread = cmd.ExecuteReader();
                while(dbread.Read())
                {
                currentclassroomID = dbread.GetString(2);
                
                }
                return currentclassroomID;
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
