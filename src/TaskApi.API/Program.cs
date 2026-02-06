using Microsoft.EntityFrameworkCore;
using TaskApi.Application.Interfaces;
using TaskApi.Application.Services;
using TaskApi.Domain.Interfaces;
using TaskApi.Infrastructure.Data;
using TaskApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var databaseProvider = builder.Configuration.GetValue<string>("DatabaseProvider");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Explicitly check for SQLite provider or .db file extension in connection string
    if (databaseProvider?.Equals("SQLite", StringComparison.OrdinalIgnoreCase) == true || 
        (builder.Environment.IsDevelopment() && connectionString?.EndsWith(".db", StringComparison.OrdinalIgnoreCase) == true))
    {
        options.UseSqlite(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString);
    }
});

// Register repositories
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Register services
builder.Services.AddScoped<ITaskService, TaskService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task Management API",
        Version = "v1",
        Description = "A simple Task Management API built with Clean Architecture"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
