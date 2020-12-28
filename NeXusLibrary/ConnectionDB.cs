using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeXusLibrary
{
    public class ConnectionDB
    {
        public static SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.Connection);
            con.Open();
            return con;
        }
    }
}
