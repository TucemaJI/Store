using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Store.BusinessLogic.Common
{
    public class Logger : ILogger
    {
        private readonly string _filePath;
        private static readonly object _lock = new object();

        public Logger(string path)
        {
            _filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            return (IDisposable)state;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter is not null)
            {
                lock (_lock)
                {
                    File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
