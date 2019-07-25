using FTS.PocoGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.PocoGenerator.Core
{
   public class Poco
    {
        public static void Cenerate(string ConnectionStringName)
        {
            Tools.GetColumnNames(ConnectionStringName);
        }
    }
}
