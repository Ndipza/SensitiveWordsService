using Swashbuckle.AspNetCore.Filters;

namespace SensitiveWords.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerExamplesFromAssemblyOf<Program>();

            services.AddSwaggerGen(options =>
            {
                options.ExampleFilters();
            });

            return services;
        }

        public static IApplicationBuilder UseApiDocumentation(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
    }
}
