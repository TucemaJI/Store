using Microsoft.AspNetCore.Http;
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
        public async Task InvokeAsync(HttpContext context)
        {
            string token = context.Request.Cookies[AdminConsts.ACCESS_TOKEN];
            if (!string.IsNullOrWhiteSpace(token))
            {
                context.Request.Headers.Add(StartupConsts.OPEN_API_AUTHORIZATION, $"Bearer {token}");
            }
            await _next.Invoke(context);
        }
    }
}
