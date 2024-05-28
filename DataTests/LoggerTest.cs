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

        internal class MockLogWriter : ILogWriter
        {
            List<LogEvent> logEvents = new List<LogEvent>();
            AutoResetEvent are;
            int counter = 0;
            public MockLogWriter(AutoResetEvent are,int logsToRecive) {
                this.are = are;
                this.counter = logsToRecive;
            }

            public List<LogEvent> LogEvents { get { return logEvents; } }
            public void appendToFile(LogEvent logEvent, string path)
            {
                logEvents.Add(logEvent);
                if(--counter == 0)
                {
                    are.Set();
                }
            }

            public void createValidFile(string path)
            {
                return;
            }

            public bool isFileValid(string path)
            {
                return true;
            }
        }

        [TestMethod]
        public void LoggerWriterTest()
        {
            AutoResetEvent are = new AutoResetEvent(false);
            MockLogWriter logWriter = new MockLogWriter(are,2);
            FileLogger logger = new FileLogger("test",logWriter);
            logger.LogInformation("test1");
            logger.LogInformation("test2");
            var wasSignaled = are.WaitOne(timeout: TimeSpan.FromSeconds(5));
            Assert.AreEqual(2, logWriter.LogEvents.Count);
            Assert.AreEqual("test1", logWriter.LogEvents[0].Message);
            Assert.AreEqual("test2", logWriter.LogEvents[1].Message);
        }
    }
}