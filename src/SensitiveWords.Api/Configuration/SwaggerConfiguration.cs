using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

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
                options.EnableAnnotations();
                options.ExampleFilters();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            return services;
        }

        public static IApplicationBuilder UseApiDocumentation(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
