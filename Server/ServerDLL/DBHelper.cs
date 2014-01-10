using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace ServerDLL
{
    class DBHelper
    {
        private SqlConnection conn;
        public SqlConnection connection
        {
            get 
            {
                conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SchoolMonitor"].ConnectionString;
                return conn;
            }
        }
    }
}
