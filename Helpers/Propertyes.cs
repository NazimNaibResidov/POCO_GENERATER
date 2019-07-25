using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.PocoGenerator.Helpers
{
   public class Propertyes
    {
        public static string CreateProperty(string type, string name)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public ");
            builder.Append(PropetyDecalre(type));
            builder.Append(" ");
            builder.Append(name);
            builder.Append(" { get; set; }");
            string str = builder.ToString();
            return builder.ToString();
        }
        private static string PropetyDecalre(string type)
        {

            if (type.Contains("Int32"))
                return "int";
            if (type.Contains("String"))
                return "string";
            if (type.Contains("Decimal"))
                return "decimal";
            if (type.Contains("Int16"))
                return "short";
            if (type.Contains("Boolean"))
                return "bool";
            if (type.Contains("Byte"))
                return "byte";
            if (type.Contains("DateTime"))
                return "DateTime";
            return type;
        }
    }
}
