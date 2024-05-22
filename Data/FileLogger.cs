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
    internal class FileLogger : ILogger
    {
        private Thread thread;
        private static Queue<LogEvent> pendingLogEvents = new Queue<LogEvent> { };
        private string filepath {  get; set; }
        private ILogWriter logWriter;


        public class LogEvent
        {
            public string Message { get; set; }
            public int eventId { get; set; }
            public string timestamp {  get; set; }
        }
        public FileLogger(string _filepath,ILogWriter writer)
        {
            thread = new Thread(new ThreadStart(writeToFileContinously));
            thread.Start();
            filepath = _filepath;
            logWriter = writer;
            if (!logWriter.isFileValid(filepath))
            {
                logWriter.createValidFile(_filepath);
            }
            
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
            logEntry.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            pendingLogEvents.Enqueue(logEntry);
        }

        private void writeToFileContinously()
        {
            while (true)
            {
                if (pendingLogEvents.Any())
                {
                    LogEvent item = pendingLogEvents.Dequeue();
                    logWriter.appendToFile(item,filepath);
                }
                Thread.Sleep(1);
            }
        }

    }
}
