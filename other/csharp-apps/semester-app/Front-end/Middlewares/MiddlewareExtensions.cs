using FrontEnd.Middlewares.Account;
using Microsoft.AspNetCore.Builder;

namespace FrontEnd.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RegistrationMiddleware>();
        }
    }
}