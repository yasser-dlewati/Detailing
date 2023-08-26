using Microsoft.AspNetCore.Authentication.JwtBearer;
using Detailing.Interfaces;
using Detailing.Services;
using Detailing.Providers;
using Detailing.Models;
using Detailing.Mappers;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
                    };
                });
builder.Services.AddAuthorization();

// Registering Services
builder.Services.AddScoped<IDatabaseService, MySqlDatabaseService>();
builder.Services.AddScoped<IModelProvider<User>, UserProvider>();
builder.Services.AddScoped<IModelProvider<Car>, CarProvider>();
builder.Services.AddScoped<IModelProvider<Business>, BusinessProvider>();
builder.Services.AddScoped<IDataMapper<User>, UserMapper>();
builder.Services.AddScoped<IDataMapper<Car>, CarMapper>();
builder.Services.AddScoped<IDataMapper<Business>, BusinessMapper>();
builder.Services.AddScoped<IAuthenticationService, JwtAuthenticationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
