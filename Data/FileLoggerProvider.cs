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

        private FileLogger logger;

        public FileLoggerProvider(string path)
        {
            
            logger = new FileLogger(path,LogWriterFactory.createJsonWriter());
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
