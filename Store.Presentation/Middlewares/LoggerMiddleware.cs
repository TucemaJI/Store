using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Exceptions;
using Store.BusinessLogic.Models.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), LoggerMiddlewareOptions.FILE_NAME));
            _logger = loggerFactory.CreateLogger(LoggerMiddlewareOptions.LOGGER);
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (BusinessLogicException exception)
            {
                var model = new BaseModel
                {
                    Errors = exception.Errors
                };
                var response = JsonSerializer.Serialize(model);
                context.Response.ContentType = LoggerMiddlewareOptions.APP_JSON;
                context.Response.StatusCode = (int)exception.Code;
                await context.Response.WriteAsync(response);
            }

            catch (Exception exception)
            {
                _logger.LogInformation(
                    exception.Message,
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
                var model = new BaseModel
                {
                    Errors = new List<string> { LoggerMiddlewareOptions.UNHANDLED_EXCEPTION, }
                };
                var response = JsonSerializer.Serialize(model);
                context.Response.ContentType = LoggerMiddlewareOptions.APP_JSON;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(response);

            }
        }
    }
}
