using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Silicion Web Api", Version = "v1" });
                x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Query,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "key",
                    Description = "Enter Api-key",
                });

                //Denna del säger till att vi behöver ha nyckeln...

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
    }
}
