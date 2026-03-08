using FluentValidation;
using SensitiveWords.Application.Validators;

namespace SensitiveWords.Api.Configuration
{
    public static class ValidationConfiguration
    {
        public static IServiceCollection AddValidationLayer(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateSensitiveWordRequestValidator>();

            services.AddScoped<ValidationFilter>();

            return services;
        }
    }
}
