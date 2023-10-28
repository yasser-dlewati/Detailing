using Microsoft.AspNetCore.Authentication.JwtBearer;
using Detailing.Interfaces;
using Detailing.Services;
using Detailing.Providers;
using Detailing.Models;
using Detailing.Mappers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Detailing.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    {
        Version = "v1", 
        Title = " Initial Version",
        Description = "This is the initial version of my Detailing app API",
        Contact = new OpenApiContact
        {
            Name = "Yasser Dlewati",
            Email= "yasdle@outlook.com",
            Url = new Uri("yasser-dlewati.github.io/me"),
        }
    });

    options.SwaggerDoc("v2", new OpenApiInfo 
    {
        Version = "v2", 
        Title = " Next Version",
        Description = "To be developed",
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
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
builder.Services.AddSingleton<IDatabaseService, MySqlDatabaseService>();
builder.Services.AddScoped<IModelProvider<User>, UserProvider>();
builder.Services.AddScoped<IModelProvider<Car>, CarProvider>();
builder.Services.AddScoped<IModelProvider<Business>, BusinessProvider>();
builder.Services.AddScoped<IModelProvider<Customer>, CustomerProvider>();
builder.Services.AddScoped<IModelProvider<Detailer>, DetailerProvider>();
builder.Services.AddScoped<IDataMapper<User>, UserMapper>();
builder.Services.AddScoped<IDataMapper<Customer>, CustomerMapper>();
builder.Services.AddScoped<IDataMapper<Detailer>, DetailerMapper>();
builder.Services.AddScoped<IDataMapper<Car>, CarMapper>();
builder.Services.AddScoped<IDataMapper<Business>, BusinessMapper>();
builder.Services.AddScoped<IModelManager<Customer>, CustomerManager>();
builder.Services.AddScoped<IModelManager<User>, UserManager>();
builder.Services.AddScoped<IModelManager<Detailer>, DetailerManager>();
builder.Services.AddScoped<IModelManager<Car>, CarManager>();
builder.Services.AddScoped<IAuthenticationService, JwtAuthenticationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    builder.Services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
    });

    builder.Services.AddVersionedApiExplorer(o =>
    {
        o.GroupNameFormat = "'v'VVV";
        o.SubstituteApiVersionInUrl = true;
    });

    // Show versions in UI dropdown
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        x.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
