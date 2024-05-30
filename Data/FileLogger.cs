using Microsoft.Extensions.Logging;

namespace Data
{
    public class FileLogger : ILogger
    {
        private readonly Thread thread;
        private static readonly Queue<LogEvent> pendingLogEvents = new() { };
        private string Filepath {  get; set; }
        private readonly ILogWriter logWriter;


        public class LogEvent(string message, int eventId, string timestamp)
        {
            public string Message { get; set; } = message;
            public int EventId { get; set; } = eventId;
            public string Timestamp { get; set; } = timestamp;
        }
        public FileLogger(string _filepath,ILogWriter writer)
        {
            thread = new Thread(new ThreadStart(WriteToFileContinously));
            thread.Start();
            Filepath = _filepath;
            logWriter = writer;
            if (!logWriter.IsFileValid(Filepath))
            {
                logWriter.CreateValidFile(_filepath);
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
            LogEvent logEntry = new(message, int.Parse(eventId.ToString()), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            pendingLogEvents.Enqueue(logEntry);
        }

        private void WriteToFileContinously()
        {
            while (true)
            {
                if (pendingLogEvents.Count != 0)
                {
                    LogEvent item = pendingLogEvents.Dequeue();
                    logWriter.AppendToFile(item,Filepath);
                }
            }
        }

    }
}
