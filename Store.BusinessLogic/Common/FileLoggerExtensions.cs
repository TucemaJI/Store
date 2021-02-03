using Microsoft.Extensions.Logging;

namespace Store.BusinessLogic.Common
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        string filePath)
        {
            factory.AddProvider(new FileLoggerProvide(filePath));
            return factory;
        }
    }
}
