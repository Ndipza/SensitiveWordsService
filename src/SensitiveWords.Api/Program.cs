using SensitiveWords.Api.Configuration;
using SensitiveWords.Application.DependencyInjection;
using SensitiveWords.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var environment = builder.Environment;

// ============================================================
// Service Registration
// ============================================================
services
    .AddApiControllers()
    .AddApiDocumentation()
    .AddApplicationLayer()
    .AddInfrastructureLayer()
    .AddValidationLayer()
    .AddApiVersioningConfiguration()
    .AddRateLimitingPolicies(environment)
    .AddHealthChecksConfiguration(configuration, environment);

var app = builder.Build();

// ============================================================
// Middleware Pipeline
// ============================================================
app
    .UseApiMiddleware()
    .UseApiSecurity(environment)
    .UseApiDocumentation(environment)
    .UseApiRateLimiting(environment);

// ============================================================
// Endpoints
// ============================================================
app.MapApiEndpoints();

app.Run();

public partial class Program { }