using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using static Data.FileLogger;

namespace Data
{
    internal class JsonLogWriter : ILogWriter
    {
        JsonSerializerOptions options = new JsonSerializerOptions();

        public JsonLogWriter(JsonSerializerOptions options)
        {
            this.options = options;
        }

        public bool isFileValid(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            try
            {
                string filedata = File.ReadAllText(path);
                List<LogEvent> events = JsonSerializer.Deserialize<List<LogEvent>>(filedata);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;

        }
        public void createValidFile(string path)
        {
            File.WriteAllText(path, "[]");
        }

        public void appendToFile(LogEvent logEvent, string path)
        {
            string jsonString = File.ReadAllText(path);
            List<LogEvent> currentContent = JsonSerializer.Deserialize<List<LogEvent>>(jsonString);
            currentContent.Add(logEvent);
            using (StreamWriter outputFile = new StreamWriter(path, append: false))
            {
                outputFile.Write(JsonSerializer.Serialize<List<LogEvent>>(currentContent, options));
            }

        }
    }
}
