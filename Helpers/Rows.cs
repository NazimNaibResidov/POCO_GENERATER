using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.PocoGenerator.Helpers
{
   public class Rows
    {
        private static void DataRows(DataRowCollection collection)
        {

            foreach (DataRow row in collection)
            {
                var type = row.ItemArray[12];
                string name = row.ItemArray[0].ToString();
               Propertyes.CreateProperty(type.ToString(), name);
            }
           

        }
    }
}
