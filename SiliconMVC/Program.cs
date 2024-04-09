using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Helpers.Middlewers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Infrastructure
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")), contextLifetime: ServiceLifetime.Transient);

            builder.Services.AddDbContext<UserContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("UserServer")), contextLifetime: ServiceLifetime.Transient);

            builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<SubscribeRepository>();
            builder.Services.AddScoped<SubscribeServices>();
            builder.Services.AddScoped<AddressServices>();
            builder.Services.AddScoped<FeatureRepository>();
            builder.Services.AddScoped<FeatureItemRepository>();
            builder.Services.AddScoped<FeatureService>();
            builder.Services.AddScoped<UserServices>();
            builder.Services.AddScoped<CourseServices>();
            builder.Services.AddScoped<CategoryServices>();
            builder.Services.AddScoped<IntegrateRepository>();
            builder.Services.AddScoped<IntegrateItemRepository>();
            builder.Services.AddScoped<IntegrateService>();
            builder.Services.AddScoped<AccountServices>();
            builder.Services.AddScoped<AddressServices>();
            builder.Services.AddScoped<AddressRepository>();
            builder.Services.AddScoped<UserManager<UserEntity>>();
            builder.Services.AddScoped<SignInManager<UserEntity>>(); 
            builder.Services.AddScoped<SubscribeRepository>();
            builder.Services.AddScoped<AccountServices>();

            builder.Services.AddHttpClient();

            builder.Services.AddDefaultIdentity<UserEntity>(x =>
             {
                 x.User.RequireUniqueEmail = true;
                 x.SignIn.RequireConfirmedAccount = false;
                 x.Password.RequiredLength = 8;
             })
                .AddRoles<IdentityRole>() //Detta gör så att vi använder identitysrole system...
                .AddEntityFrameworkStores<UserContext>();

            builder.Services.ConfigureApplicationCookie(x =>
            {
                //Detta förhindrar att någon kan läsa ut cookieinformationen
                x.Cookie.HttpOnly = true; //Webbläsaren kan inte hämta information från min cookie...
                x.LoginPath = "/signin";
                x.LogoutPath = "/signout";
                x.AccessDeniedPath = "/denied";
                x.Cookie.SecurePolicy = CookieSecurePolicy.Always; //Allt ska gå via https...
                //Användaren loggar ut automatisk efter 30 min...
                x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                //Denna del gör så att ExpireTime nollställ så for en sida laddas om...
                x.SlidingExpiration = true;
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("SuperAdmins", policy => policy.RequireRole("SuperAdmin"));
                x.AddPolicy("CIO", policy => policy.RequireRole("SuperAdmin", "CIO"));
                x.AddPolicy("Admins", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin"));
                x.AddPolicy("Managers", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "Manager"));
                x.AddPolicy("AuthenticatedUsers", policy => policy.RequireRole("SuperAdmin", "CIO", "Admin", "Manager", "Users"));
            });

            //builder.Services.AddAuthentication(x =>
            //{
            //    x.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme =GoogleDefaults.AuthenticationScheme;
            //})
            //    .AddCookie(x =>
            //    x.LoginPath = "/signin")
            //    .AddGoogle(GoogleDefaults.AuthenticationScheme, x =>
            //    {
            //        x.ClientId = "281613275639-gujppvlp3tqdbabu322e7qp2e9jo6s6j.apps.googleusercontent.com";
            //        x.ClientSecret = "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";
            //        });

            //Lägg till Facebook auth...

            builder.Services.AddAuthentication().AddFacebook(x =>
            {
                x.AppId = "1199838401400858";
                x.AppSecret = "d631e6040dc68772c8d1062f09267c9a";
                x.Fields.Add("first_name");
                x.Fields.Add("last_name");
            });

            var app = builder.Build();

            app.UseCors(x =>
            x.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());

            //Middleweres - gös saker innan sidan laddas...
            //app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseUserSessionValidation();
            app.UseAuthentication(); //Vem är du - vi har ett formulär som skickas till en server med en Post?
            app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
            app.UseAuthorization(); // Vad får du göra?

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = ["SuperAdmin", "CIO", "Admin", "Manager", "User"];

                foreach(var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
