using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ArtVista.Infrastructure.Context;
using ArtVista.Application.DTOs;
using ArtVista.Identity.Model;
using ArtVista.Identity.Context;
using ArtVista.Application.Interfaces;
using ArtVista.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Get connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");



// ✅ Configure ArtIdentityDbContext
builder.Services.AddDbContext<ArtIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Register Identity with ArtIdentityDbContext
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ArtIdentityDbContext>()
    .AddDefaultTokenProviders();

// ✅ Register AuthService
builder.Services.AddScoped<IAuthService, AuthService>();

// ✅ Configure JWT Authentication
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettingsSection);

var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
if (string.IsNullOrEmpty(jwtSettings?.Key))
{
    throw new Exception("JWT Key is missing from configuration.");
}

var key = Encoding.UTF8.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ✅ Add Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
