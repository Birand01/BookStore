using BackEnd.Extensions;
using BackEnd.Repositories.EFCore;
using BackEnd.Repositories.Contracts;
using BackEnd.Repositories;
using BackEnd.Services.Contracts;
using BackEnd.Services.Managers;
using Microsoft.EntityFrameworkCore;
using BackEnd.ActionFilters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers(config=>{
    config.RespectBrowserAcceptHeader=true;//accept header
    config.ReturnHttpNotAcceptable=false; //return 406 not acceptable if the client request is not acceptable
    config.CacheProfiles.Add("300SecondsCache",new CacheProfile{Duration=300});
})
//.AddCustomCsvFormatter()
//.AddXmlDataContractSerializerFormatters()
.AddNewtonsoftJson();


builder.Services.AddScoped<ValidationFilterAttribute>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.ConfigurePostgresContext(builder.Configuration);

// Register Repository Manager
builder.Services.ConfigureRepositoryManager();
// Register Service Manager
builder.Services.ConfigureServiceManager(); 

// Register Logger
builder.Services.AddSingleton<ILoggerService, LoggerManager>();

// Register Response Caching
builder.Services.ConfigureResponseCaching();
// Register Cors
builder.Services.ConfigureCors();
// Register Http Cache Headers
builder.Services.ConfigureHttpCacheHeaders();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseResponseCaching();
app.UseHttpCacheHeaders();
app.UseAuthorization();
app.MapControllers();

app.Run();
