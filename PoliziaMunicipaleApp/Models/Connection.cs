using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PoliziaMunicipaleApp.Models
{
    public class Connection
    {
        public static SqlConnection GetConnection()
        {
            string conString = ConfigurationManager.ConnectionStrings["MunicipaleDB"].ToString();
            SqlConnection con = new SqlConnection(conString);
            return con;
        }

        public static SqlDataReader GetReader(string commandtext, SqlConnection con) 
        {
            SqlCommand command = new SqlCommand(commandtext, con);
            SqlDataReader reader = command.ExecuteReader();
            return reader;
        }
    }
}