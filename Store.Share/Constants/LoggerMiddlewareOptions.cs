namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public class LoggerMiddlewareOptions
        {
            public const string FileName = "logger.txt";
            public const string Logger = "Logger";
            public const string RequestForm = "Request {method} {url} => {statusCode}";
            public const string AppJson = "application/json";
        }
    }
}
