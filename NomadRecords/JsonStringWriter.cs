using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data;

using Newtonsoft.Json;

namespace NomadRecords
{
    class JsonStringWriter
    {
        public void DataTableToJSONWithJSONNet(DataTable table, string stokvelIdkey, string entity,  string tableName)
        {
            string direc = System.AppDomain.CurrentDomain.BaseDirectory + @"\data\" + entity + @"\" + stokvelIdkey + @"\";

            System.IO.Directory.CreateDirectory(direc);

            string direcs = direc + tableName + ".json";

            // serialize JSON to a string and then write string to a file
            File.WriteAllText(direcs, JsonConvert.SerializeObject(table));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(direcs))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, table);
            }
        }
    }
}
