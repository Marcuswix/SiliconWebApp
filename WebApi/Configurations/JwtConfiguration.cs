using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Configurations
{
    //Static gör att det inte går att skapa instanse av denna klass... 
    public static class JwtConfiguration
    {
        //En Jwt är viktig för att säker kommunitkation och authensiserign, som ett digitalt Id-kort...

        //IServiceCollection samlar alla appens tjänster ex. databasanrop och autentisering... Det som ger ".Services"

        //IConfiguration gör så att man kan komma åt API-nycklar, databaser och anslutninsgsträngar... Det som ger ".Configuration"

        //this gör så att du lägger till din funktion i IserviceCollection
        public static void RegisterJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
             {
                 x.TokenValidationParameters = new TokenValidationParameters
                 {
                     //Vem är utgivaren
                     ValidateIssuer = true,
                     ValidIssuer = configuration["Jwt:Issuer"],

                     //Vem är mottagaren
                     ValidateAudience = true,
                     ValidAudience = configuration["Jwt:Audience"],

                     ValidateLifetime = true,

                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),

                     ClockSkew = TimeSpan.FromSeconds(5)
                 };
             });
        }

        //public static void RegisterJwtTwo(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddAuthentication(x =>
        //    {
        //        //Säger att när vi sätter ett [Authorizie] ska vi använda detta "attributet"...
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        //Säger att vi kräver en token authentisering tillbaka...
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        //    }).AddJwtBearer(x =>
        //    {
        //        //Vilka parameterar som vi ska ta emot... Vad måste finnas med i Tokennyckeln för att det ska vara gitligt...
        //        x.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            //Vem är den giltliga utfärdaren? [Denna denl går in i appsettings och hämtar denna del...]
        //            ValidateIssuer = true,
        //            ValidIssuer = configuration["Token:Issuer"],

        //            //Vem ska bruka detta?
        //            ValidateAudience = true,
        //            ValidAudience = configuration["Token:Audience"],

        //            //Att vi vill ha en nyckel att validera med... Encoding.UFT8 gör så att vi kan ha med å, ä, ö i vår nyckel, .GetBytes() gör så att vi konverterar om till en byte-array... som krävs. 
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]!)),

        //            //Validera livslängden på tokennycklen...
        //            //iat = issued att
        //            //nbf = not before 2024-01-01 00:00:00
        //            ValidateLifetime = true,

        //            //Olika klockor kan variera något, därför så tillåter man ett tidsspan att existera... detta blir också en säkerhetsrisk - ju mindre tid ju säkrare... 
        //            ClockSkew = TimeSpan.Zero

        //        };
        //    });
        //}
    }
}
