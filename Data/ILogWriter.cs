using static Data.FileLogger;

namespace Data
{
    public interface ILogWriter
    {
        public void AppendToFile(LogEvent logEvent, string path);
        public void CreateValidFile(string path);
        public bool IsFileValid(string path);
    }
}
