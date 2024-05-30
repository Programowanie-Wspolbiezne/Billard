using System.Text.Json;
using static Data.FileLogger;

namespace Data
{
    internal class JsonLogWriter(JsonSerializerOptions options) : ILogWriter
    {
        readonly JsonSerializerOptions options = options;

        public bool IsFileValid(string path)
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
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        public void CreateValidFile(string path)
        {
            File.WriteAllText(path, "[]");
        }

        public void AppendToFile(LogEvent logEvent, string path)
        {
            string jsonString = File.ReadAllText(path);
            List<LogEvent> currentContent = JsonSerializer.Deserialize<List<LogEvent>>(jsonString);
            currentContent.Add(logEvent);
            using StreamWriter outputFile = new(path, append: false);
            outputFile.Write(JsonSerializer.Serialize<List<LogEvent>>(currentContent, options));

        }
    }
}
