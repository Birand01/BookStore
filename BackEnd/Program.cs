using BackEnd.Extensions;
using BackEnd.Repositories.EFCore;
using BackEnd.Repositories.Contracts;
using BackEnd.Repositories;
using BackEnd.Services.Contracts;
using BackEnd.Services.Managers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.ConfigurePostgresContext(builder.Configuration);

// Register Repository Manager
builder.Services.ConfigureRepositoryManager();
// Register Service Manager
builder.Services.ConfigureServiceManager(); 

// Register Logger
builder.Services.AddSingleton<ILoggerService, LoggerManager>();

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
app.UseAuthorization();
app.MapControllers();

app.Run();
