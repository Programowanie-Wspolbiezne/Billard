using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    internal abstract class LogWriterFactory
    {
        public static Data.ILogWriter createJsonWriter()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            return new Data.JsonLogWriter(options);
        }
    }
}
