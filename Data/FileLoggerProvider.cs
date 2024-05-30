using Microsoft.Extensions.Logging;

namespace Data
{
    public class FileLoggerProvider(string path) : ILoggerProvider
    {

        private readonly FileLogger logger = new(path, LogWriterFactory.CreateJsonWriter());

        public ILogger CreateLogger(string categoryName)
        {
            return logger;
        }

        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
