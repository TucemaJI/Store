﻿using Microsoft.Extensions.Logging;

namespace Store.BusinessLogic.Common
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _path;
        public FileLoggerProvider(string path)
        {
            _path = path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(_path);
        }

        public void Dispose()
        {
        }
    }
}
