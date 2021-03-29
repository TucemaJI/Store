using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.Presentation.Middlewares
{
    public class TokenMiddleware
    {
        private RequestDelegate _next;
        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Cookies[AdminConsts.ACCESS_TOKEN];
            if (!string.IsNullOrWhiteSpace(token))
            {
                context.Request.Headers.Add("Authorization", $"Bearer {token}");
            }
            await _next.Invoke(context);
        }
    }
}
