using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private FileLogger logger = new FileLogger();

        public FileLoggerProvider()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "WriteTextAsync.json");
            using (StreamWriter outputFile = new StreamWriter(docPath, append: false))
            {
                List<string> list = new List<string>();
                outputFile.Write(JsonSerializer.Serialize<List<string>>(list));
            }

        }

        public ILogger CreateLogger(string categoryName)
        {
            return logger;
        }

        public void Dispose()
        {
            
        }
    }
}
