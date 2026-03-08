using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SensitiveWords.Application.DTOs.SensitiveWords;
using SensitiveWords.Tests.Integration;
using SensitiveWords.Tests.Integration.TestHelpers;
using System.Net;
using System.Net.Http.Json;

namespace SensitiveWords.Tests.Integration.Controllers
{
    public class SensitiveWordsControllerTests : IntegrationTestBase
    {
        public SensitiveWordsControllerTests(CustomWebApplicationFactory factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetAll_ShouldReturnWords()
        {
            var response = await Client.GetAsync("/api/v1/sensitive-words");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<List<SensitiveWordResponse>>();

            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Create_ShouldAddSensitiveWord()
        {
            var request = new CreateSensitiveWordRequest
            {
                Word = "INSERT"
            };

            var response = await Client.PostAsJsonAsync(
                "/api/v1/sensitive-words",
                request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var getResponse = await Client.GetAsync("/api/v1/sensitive-words");

            var words = await getResponse.ReadJsonAsync<List<SensitiveWordResponse>>();

            words.Any(w => w.Word == "INSERT").Should().BeTrue();
        }

        [Fact]
        public async Task GetAll_ShouldContainNewWord()
        {
            var request = new CreateSensitiveWordRequest
            {
                Word = "INSERT"
            };

            await Client.PostAsJsonAsync("/api/v1/sensitive-words", request);

            var response = await Client.GetAsync("/api/v1/sensitive-words");

            response.EnsureSuccessStatusCode();

            var words = await response.Content
                .ReadFromJsonAsync<List<SensitiveWordResponse>>();

            words!.Any(w => w.Word == "INSERT").Should().BeTrue();
        }

        [Fact]
        public async Task Update_ShouldModifySensitiveWord()
        {
            var create = new CreateSensitiveWordRequest
            {
                Word = "OLDWORD"
            };

            await Client.PostAsJsonAsync("/api/v1/sensitive-words", create);

            var listResponse = await Client.GetAsync("/api/v1/sensitive-words");
            var words = await listResponse.ReadJsonAsync<List<SensitiveWordResponse>>();

            var created = words.First(w => w.Word == "OLDWORD");

            var update = new UpdateSensitiveWordRequest
            {
                Word = "UPDATEDWORD"
            };

            var updateResponse = await Client.PutAsJsonAsync(
                $"/api/v1/sensitive-words/{created.Id}",
                update);

            updateResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var verifyResponse = await Client.GetAsync("/api/v1/sensitive-words");
            var updatedWords = await verifyResponse.ReadJsonAsync<List<SensitiveWordResponse>>();

            updatedWords.Any(w => w.Word == "UPDATEDWORD").Should().BeTrue();
        }

        [Fact]
        public async Task Delete_ShouldRemoveSensitiveWord()
        {
            var request = new CreateSensitiveWordRequest
            {
                Word = "TEMPWORD"
            };

            await Client.PostAsJsonAsync("/api/v1/sensitive-words", request);

            var listResponse = await Client.GetAsync("/api/v1/sensitive-words");
            var words = await listResponse.ReadJsonAsync<List<SensitiveWordResponse>>();

            var created = words.First(w => w.Word == "TEMPWORD");

            var deleteResponse = await Client.DeleteAsync(
                $"/api/v1/sensitive-words/{created.Id}");

            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var verifyResponse = await Client.GetAsync("/api/v1/sensitive-words");
            var updatedWords = await verifyResponse.ReadJsonAsync<List<SensitiveWordResponse>>();

            updatedWords.Any(w => w.Word == "TEMPWORD").Should().BeFalse();
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenWordDoesNotExist()
        {
            var request = new UpdateSensitiveWordRequest
            {
                Word = "NEWWORD"
            };

            var response = await Client.PutAsJsonAsync(
                "/api/v1/sensitive-words/999",
                request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenWordDoesNotExist()
        {
            var response = await Client.DeleteAsync("/api/v1/sensitive-words/999");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();

            error!.Title.Should().Be("Resource not found");
        }
    }
}