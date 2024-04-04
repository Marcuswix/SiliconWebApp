using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers.Middlewers
{
    public class UserSessionValidationMiddlewere
    {
        private readonly RequestDelegate _requestDelegate;

        public UserSessionValidationMiddlewere(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        private static bool IsAjaxRequest(HttpRequest request)
        {
            return request.Headers.XRequestedWith == "XMLHttpRequest";
        }

        public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            if(context.User.Identity.IsAuthenticated)
            {
                    var user = await userManager.GetUserAsync(context.User);

                    if(user == null)
                    {
                        await signInManager.SignOutAsync();

                        if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                        {
                            var signInPath = "/signin";
                            context.Response.Redirect(signInPath);
                            return;
                        }
                    }
            }
            
            await _requestDelegate(context);
        }
    }
}
