using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;

namespace Infrastructure.Helpers.Middlewers
{
    public static class BuilderApplicationExtionsions
    {
        public static IApplicationBuilder UseUserSessionValidation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserSessionValidationMiddlewere>();
        }
    }
}
