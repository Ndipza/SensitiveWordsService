using Microsoft.Extensions.DependencyInjection;
using SensitiveWords.Application.Interfaces;
using SensitiveWords.Infrastructure.Database;
using SensitiveWords.Infrastructure.Repositories;

namespace SensitiveWords.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<DbConnectionFactory>();

            services.AddScoped<ISensitiveWordRepository, SensitiveWordRepository>();

            return services;
        }
    }
}
