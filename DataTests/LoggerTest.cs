using Data;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Net;
using static Data.FileLogger;

namespace DataTests
{
    [TestClass]
    public class LoggerTest
    {

        internal class MockLogWriter(AutoResetEvent are, int logsToRecive) : ILogWriter
        {
            readonly List<LogEvent> logEvents = [];
            readonly AutoResetEvent are = are;
            int counter = logsToRecive;

            public List<LogEvent> LogEvents { get { return logEvents; } }
            public void AppendToFile(LogEvent logEvent, string path)
            {
                logEvents.Add(logEvent);
                if(--counter == 0)
                {
                    are.Set();
                }
            }

            public void CreateValidFile(string path)
            {
                return;
            }

            public bool IsFileValid(string path)
            {
                return true;
            }
        }

        [TestMethod]
        public void LoggerWriterTest()
        {
            AutoResetEvent are = new(false);
            MockLogWriter logWriter = new(are,2);
            FileLogger logger = new("test",logWriter);
            logger.LogInformation("test1");
            logger.LogInformation("test2");
            are.WaitOne(timeout: TimeSpan.FromSeconds(5));
            Assert.AreEqual(2, logWriter.LogEvents.Count);
            Assert.AreEqual("test1", logWriter.LogEvents[0].Message);
            Assert.AreEqual("test2", logWriter.LogEvents[1].Message);
        }
    }
}