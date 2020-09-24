using Microsoft.Extensions.Logging;

namespace Store.BusinessLogic.Common
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string path;
        public FileLoggerProvider(string _path)
        {
            path = _path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(path);
        }

        public void Dispose()
        {
        }
    }
}
