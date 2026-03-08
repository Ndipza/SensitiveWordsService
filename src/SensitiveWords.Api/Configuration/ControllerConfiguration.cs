namespace SensitiveWords.Api.Configuration
{
    public static class ControllerConfiguration
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ValidationFilter>();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = false;
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddScoped<ValidationFilter>();

            return services;
        }
    }
}
