using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;
using SensitiveWords.Infrastructure.Repositories;
using System.Threading.RateLimiting;
using SensitiveWords.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Infrastructure
builder.Services.AddInfrastructure();

// Dependency Injection
builder.Services.AddScoped<ISensitiveWordRepository, SensitiveWordRepository>();
builder.Services.AddScoped<ISensitiveWordService, SensitiveWordService>();
builder.Services.AddScoped<SanitizationService>();

builder.Services.AddSingleton<SensitiveWordEngine>();
builder.Services.AddHostedService<SensitiveWordEngineLoader>();

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("SanitizePolicy", opt =>
    {
        opt.PermitLimit = 100;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 10;
    });
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.MapControllers();

app.MapHealthChecks("/health/live");

app.MapHealthChecks("/health/ready");

app.Run();