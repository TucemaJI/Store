﻿using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Store.BusinessLogic.Common
{
    public class Logger : ILogger
    {
        private string filePath;
        private static object _lock = new object();

        public Logger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var test = logLevel;
            if (exception == null)
            {
                return;
            }
                lock (_lock)
                {
                    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                }

            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                }
            }
            //if (logLevel == LogLevel.Warning)
            //{
            //    lock (_lock)
            //    {
            //        File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
            //    }
            //}
        }
    }
}
