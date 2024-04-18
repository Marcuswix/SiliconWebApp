using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDbContext<UserContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UserServer")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registera din swagger konfiguration...
builder.Services.RegisterSwagger();

//Registera din Jwt
builder.Services.RegisterJwt(builder.Configuration);

//builder.Services.AddSession(x =>
//{
//    x.IdleTimeout = TimeSpan.FromMinutes(20);
//    x.Cookie.IsEssential = true;
//    x.Cookie.HttpOnly = true;
//});

builder.Services.AddScoped<SubscriberEntity>();
builder.Services.AddScoped<SubscribeRepository>();
builder.Services.AddScoped<CoursesRepository>();
builder.Services.AddScoped<CourseEntity>();
builder.Services.AddScoped<UserContext>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CourseDetailsRepository>();
builder.Services.AddScoped<TeacherRepository>();
builder.Services.AddScoped<UserManager<UserEntity>>();
builder.Services.AddScoped<SignInManager<UserEntity>>();
builder.Services.AddScoped<CourseDetailsRepository>();
builder.Services.AddScoped<MyCoursesReporsitory>();
builder.Services.AddScoped<ContactRepository>();


builder.Services.AddIdentity<UserEntity, IdentityRole>(options =>
{
})
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});

//builder.Services.AddCors(x => 
//x.AddPolicy("CustomerOriginPolicy", policy =>
//{
//    policy.WithOrigins("")
//    .AllowAnyMethod()
//    .AllowAnyHeader();
//}) );

var app = builder.Build();

//AllowAnyHeaders är att vi vill tillåta alla keys, m.m., AllowAnyOrigin gör att alla får komma åt APIet, AllowAnyMethod tillåter alla metoder (post, get, ...)
app.UseCors(x =>
x.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicion Web Api v1"));
}

//app.UseSession();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
