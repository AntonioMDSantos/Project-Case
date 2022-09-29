using Back.Application.Services;
using Back.Application.Services.Interfaces;
using Back.Domain.Entities;
using Back.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rotativa.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductOrderService, ProductOrderService>();
builder.Services.AddScoped<IRecoveryEmailService, RecoveryEmailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
            optionsBuilder.UseMySql(builder.Configuration.GetConnectionString("localMySqlConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("localMySqlConnection")))
);

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = String.Empty;
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddRoleManager<RoleManager<IdentityRole>>()
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

string security_key = builder.Configuration.GetSection("TokenAuthentication")["SecretKey"];
var symetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(security_key));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
    options.TokenValidationParameters.IssuerSigningKey = symetricSecurityKey;
    options.TokenValidationParameters.ValidAudience = builder.Configuration.GetSection("TokenAuthentication")["Audience"];
    options.TokenValidationParameters.ValidIssuer = builder.Configuration.GetSection("TokenAuthentication")["Issuer"];
    options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
});

var app = builder.Build();

RotativaConfiguration.Setup((Microsoft.AspNetCore.Hosting.IHostingEnvironment)app.Environment, "Rotativa");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
