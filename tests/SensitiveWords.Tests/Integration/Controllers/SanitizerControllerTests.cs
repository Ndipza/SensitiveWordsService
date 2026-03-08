using System.Net.Http.Json;
using FluentAssertions;
using SensitiveWords.Application.DTOs.Sanitization;

namespace SensitiveWords.Tests.Integration.Controllers
{
    public class SanitizerControllerTests : IntegrationTestBase
    {
        public SanitizerControllerTests(CustomWebApplicationFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Sanitize_ShouldMaskSensitiveWords()
        {
            var request = new SanitizeRequest
            {
                Input = "SELECT * FROM USERS"
            };

            var response = await Client.PostAsJsonAsync("/api/v1/sanitizer", request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SanitizeResponse>();

            result!.Output.Should().Contain("******");
        }
    }
}