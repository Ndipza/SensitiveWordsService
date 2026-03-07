using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Api.Filters;
using SensitiveWords.Api.Middleware;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Application.Services;
using SensitiveWords.Application.Validator;
using SensitiveWords.Infrastructure;
using SensitiveWords.Infrastructure.DependencyInjection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddSingleton<ISensitiveWordRepository, InMemorySensitiveWordRepository>();
}

// Controllers + Validation Filter
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
})
.AddJsonOptions(o =>
{
    o.JsonSerializerOptions.DefaultBufferSize = 16 * 1024;
});

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultBufferSize = 16 * 1024;
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure (DB, repositories etc.)
builder.Services.AddInfrastructure();

// Application Services
builder.Services.AddScoped<ISensitiveWordService, SensitiveWordService>();
builder.Services.AddScoped<SanitizationService>();

// Trie Engine (Singleton)
builder.Services.AddSingleton<ISensitiveWordEngine, SensitiveWordEngine>();

// Background loader (loads words on startup)
builder.Services.AddHostedService<SensitiveWordEngineLoader>();

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateSensitiveWordRequestValidator>();

// Filters
builder.Services.AddScoped<ValidationFilter>();

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
builder.Services
    .AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware Pipeline
app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Endpoints
app.MapControllers();

app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");

app.Run();

public partial class Program { }