using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public class FileLogger : ILogger
    {
        private Thread thread;
        private static Queue<LogEvent> pendingLogEvents = new Queue<LogEvent> { };


        private class LogEvent
        {
            public string Message { get; set; }
            public int eventId { get; set; }
        }
        public FileLogger()
        {
            thread = new Thread(new ThreadStart(writeToFileContinously));
            thread.Start();
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return default;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = formatter(state, exception);
            LogEvent logEntry = new LogEvent();
            logEntry.Message = message;
            logEntry.eventId = int.Parse(eventId.ToString());
            pendingLogEvents.Enqueue(logEntry);
        }
        private void writeToFileContinously()
        {
            while(true)
            {
                if (pendingLogEvents.Any()) {
                    LogEvent item = pendingLogEvents.Dequeue();
                    writeToFile(item);
                }
                Thread.Sleep(1);
            }
        }
        private void writeToFile(LogEvent logEvent)
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            docPath = Path.Combine(docPath, "WriteTextAsync.json");
            string jsonString = File.ReadAllText(docPath);
            List<LogEvent> currentContent = JsonSerializer.Deserialize<List<LogEvent>>(jsonString);
            currentContent.Add(logEvent);
            using (StreamWriter outputFile = new StreamWriter(docPath, append: false))
            {
                outputFile.Write(JsonSerializer.Serialize<List<LogEvent>>(currentContent));
            }
        
        }
    }
}
