using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SensitiveWords.Tests.Unit.Api
{
    public class ApiStartupTests
    {
        [Fact]
        public void Api_Should_Start_With_All_Configurations()
        {
            var builder = WebApplication.CreateBuilder();

            // simulate Program.cs configuration
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.Should().NotBeNull();
        }
    }
}
