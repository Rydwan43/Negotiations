using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Negotiations.Application;
using Negotiations.Application.Settings;
using Negotiations.Infrastructure;
using Negotiations.Infrastructure.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

var jwtSettings = new JwtSettings {
                JwtKey = configuration["Authentication:JwtKey"],
                JwtExpireDays = int.Parse(configuration["Authentication:JwtExpireDays"]),
                JwtIssuer = configuration["Authentication:JwtIssuer"]
            };

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.JwtIssuer,
        ValidAudience = jwtSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey)),
    };
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
    In = ParameterLocation.Header, 
    Description = "Please insert JWT with Bearer into field",
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey 
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    { 
        new OpenApiSecurityScheme 
        { 
            Reference = new OpenApiReference 
            { 
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer" 
            } 
        },
        new string[] { } 
    } 
    });
});

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<NegotiationsSeeder>();

seeder.Seed();

app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();


app.MapControllers();

app.Run();
