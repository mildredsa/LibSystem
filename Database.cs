﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibSystem
{
    public static class Database
    {
        public static string connection = "Data Source=LAPTOP-M3IQH77K\\MSASERVER;Initial Catalog=LibSystem;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(connection);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return con;
        }
    }
}
