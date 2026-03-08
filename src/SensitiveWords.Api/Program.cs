using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using SensitiveWords.Api.Middleware;
using SensitiveWords.Application.DependencyInjection;
using SensitiveWords.Application.HealthChecks;
using SensitiveWords.Application.Validators;
using SensitiveWords.Infrastructure.DependencyInjection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//
// Controllers + Validation Filter
//
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
    })
    .AddJsonOptions(options =>
    {
        // Force MVC JSON serializer pipeline (avoids PipeWriter issue in TestServer)
        options.JsonSerializerOptions.WriteIndented = false;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

//
// Swagger
//
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// Application + Infrastructure
//
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

//
// FluentValidation
//
builder.Services.AddValidatorsFromAssemblyContaining<CreateSensitiveWordRequestValidator>();

//
// Filters
//
builder.Services.AddScoped<ValidationFilter>();

//
// Rate Limiting
//
if (!builder.Environment.IsEnvironment("Testing"))
{
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
}

//
// Health Checks
//
var healthChecks = builder.Services.AddHealthChecks();

healthChecks.AddCheck<TrieHealthCheck>("trie");

if (!builder.Environment.IsEnvironment("Testing"))
{
    healthChecks.AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
}

//
// API Versioning
//
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

var app = builder.Build();

//
// Authentication & Authorization (disabled for Integration Tests)
//
if (!builder.Environment.IsEnvironment("Testing"))
{
    app.UseAuthentication();
    app.UseAuthorization();
}

//
// Swagger
//
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//
// HTTPS
//
app.UseHttpsRedirection();

//
// Middleware (disabled for Integration Tests)
//
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

//
// Rate Limiter
//
if (!app.Environment.IsEnvironment("Testing"))
{
    app.UseRateLimiter();
}

//
// Endpoints
//
app.MapControllers();

//
// Health Endpoints
//
app.MapHealthChecks("/health/live");
app.MapHealthChecks("/health/ready");

app.Run();

public partial class Program { }