using System.Security.Claims;
using System.Threading.Tasks;
using FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FrontEnd.Middlewares.Account
{
    public class RegistrationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserService _userService;

        public RegistrationMiddleware(RequestDelegate next, UserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            var controllerContext = httpContext.GetEndpoint();
            if (controllerContext != null)
            {
                var metadata = controllerContext.Metadata;
                var controllerActionDescriptor = metadata.GetMetadata<ControllerActionDescriptor>();

                var response = httpContext.Response;
                var user = httpContext.User;
                var identity = user.Identity;

                if (identity.IsAuthenticated && user.IsInRole("Student"))
                {
                    if (!await _userService.IsCurrentUserRegistered())
                    {
                        if (controllerActionDescriptor.ControllerName != "User" || controllerActionDescriptor.ActionName != "Register")
                        {
                            response.Redirect("/User/Register", false); 
                        }
                    }
                }   
            }
            await _next(httpContext);

        }
        
    }
}