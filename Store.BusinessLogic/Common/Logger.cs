
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
            return NullScope.Instance;
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
        private sealed class NullScope : IDisposable
        {
            public static NullScope Instance { get; } = new NullScope();

            private NullScope() { }

            public void Dispose() { }
        }
    }
}
