using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.PocoGenerator.Helpers
{
   public class Tools
    {
        private const string _SELECT = "SELECT * FROM ";
        private const string _WHERE = " WHERE 1=0";
        private static void Save(string path, string name, string values)
        {

            FileStream outputFileStream = new FileStream(path + $"{name}.cs", FileMode.Append, FileAccess.Write);
            StreamWriter write = new StreamWriter(outputFileStream);
            write.Write(values);
            write.Close();
            outputFileStream.Close();
        }
        public static string GetColumnNames(string connectionName)
        {
            Tools tools = new Tools();
            string namespaces = tools.Namespacer();
            string pathas = tools.Paths();
            ConfigurationManager.ConnectionStrings[connectionName].ToString();

            StringBuilder builder = new StringBuilder();

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connectionName].ToString()))
            {

                connection.Open();
                string ClassName = string.Empty;
                foreach (var item in Tables.ListTables(connectionName))
                {

                    SqlCommand sqlCmd = connection.CreateCommand();
                    if (item.Contains(" "))
                    {
                        continue;
                    }
                    else if (item.Contains("__EFMigrationsHistory"))
                    {
                        continue;
                    }

                    ClassName = item;
                    sqlCmd.CommandText = _SELECT + item + _WHERE;


                    builder.Append("using System;\n");
                    builder.Append("\n\n");
                    builder.Append("namespace " + namespaces + ".Models \n{\n");
                    builder.Append("\n     public class " + ClassName + " {\n\n");

                    sqlCmd.CommandType = CommandType.Text;
                    var sqlDR = sqlCmd.ExecuteReader();
                    var dataTable = sqlDR.GetSchemaTable();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        var type = row.ItemArray[12];
                        string name = row.ItemArray[0].ToString();
                        string mss = Propertyes.CreateProperty(type.ToString(), name);
                        builder.Append("     "+mss + "\n");


                    }
                    builder.Append("\n}\n}");
                    if (!Directory.Exists(pathas + "/Models"))
                    {


                        Directory.CreateDirectory(pathas + "/Models");


                    }




                    Save(pathas + "/Models/", ClassName, builder.ToString());
                    builder.Clear();
                    sqlCmd.Clone();
                }
            }
            string ms = builder.ToString();

            return builder.ToString();
        }
        private string Namespacer()
        {
            string str= GetType().Namespace.Replace(".Helpers", string.Empty);
            return str;
        }
        private string Paths()
        {
            return AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug", string.Empty);
        }
    }
}
