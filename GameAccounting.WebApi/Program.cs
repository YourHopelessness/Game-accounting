using System.Reflection;
using GameAccounting.DAL;
using GameAccounting.BL.Interfaces;
using GameAccounting.BL.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using GameAccounting.BL.Mappers;
using GameAccounting.WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

var envorimentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
builder.Configuration.AddJsonFile($"appsetting.{envorimentName}.json");

builder.Services
        .AddControllers(c =>
        {
            c.Filters.Add<ExceptionHandlerFilter>();
        })
        .AddNewtonsoftJson(o =>
        {
            o.SerializerSettings.Converters.Add(
                new StringEnumConverter
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                });
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Description = "Web Api для управленя информацией о видеоиграх",
        Title = "Учет видеоигр"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

// сервисы в IoC
builder.Services.AddDbContext<GamesContext>(opt => 
        opt.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IDeveloperService, DeveloperService>();
builder.Services.AddAutoMapper(typeof(GameMappingProfile));


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
