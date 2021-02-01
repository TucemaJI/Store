﻿namespace Store.Shared.Constants
{
    public partial class Constants
    {
        public class LoggerMiddlewareOptions
        {
            public const string FILE_NAME = "logger.txt";
            public const string LOGGER = "Logger";
            public const string REQUEST_FORM = "Request {method} {url} => {statusCode}";
            public const string APP_JSON = "application/json";
        }
    }
}
