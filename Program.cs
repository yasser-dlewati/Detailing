using Detailing.Interfaces;
using Detailing.Services;
using Detailing.Repositories;
using Detailing.Models;
using Detailing.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering Services
builder.Services.AddScoped<IDatabaseService, MySqlDatabaseService>();
builder.Services.AddScoped<IRepositoryService<User>, UserRepository>();
builder.Services.AddScoped<IRepositoryService<Car>, CarRepository>();
builder.Services.AddScoped<IRepositoryService<Business>, BusinessRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
