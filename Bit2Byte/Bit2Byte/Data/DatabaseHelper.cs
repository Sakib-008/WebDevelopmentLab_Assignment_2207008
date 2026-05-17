using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Bit2Byte.Data
{
    public static class DatabaseHelper
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["Bit2ByteConnection"]?.ConnectionString;

        public static SqlConnection GetOpenConnection()
        {
            var cs = ConnectionString;
            if (string.IsNullOrEmpty(cs))
                throw new InvalidOperationException("Connection string 'Bit2ByteConnection' not found in Web.config.");

            var conn = new SqlConnection(cs);
            conn.Open();
            return conn;
        }
    }
}
