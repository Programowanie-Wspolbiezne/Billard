using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.FileLogger;

namespace Data
{
    public interface ILogWriter
    {
        public void appendToFile(LogEvent logEvent, string path);
        public void createValidFile(string path);
        public bool isFileValid(string path);
    }
}
