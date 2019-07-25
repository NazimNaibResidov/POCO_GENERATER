using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FTS.PocoGenerator.Helpers
{
    public class Tables
    {
        public static IList<string> ListTables(string _connectionName)
        {

         string conn=   ConfigurationManager.ConnectionStrings[_connectionName].ToString();
            List<string> tables = new List<string>();

            using (var _connection = new SqlConnection(conn))
            {
                _connection.Open();
                DataTable dt = _connection.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                }
            }

            return tables;
        }
    }
}
