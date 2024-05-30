using System.Text.Json;

namespace Data
{
    internal abstract class LogWriterFactory
    {
        public static ILogWriter CreateJsonWriter()
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };
            return new JsonLogWriter(options);
        }
    }
}
