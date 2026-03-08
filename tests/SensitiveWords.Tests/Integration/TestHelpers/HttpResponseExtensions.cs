using System.Net.Http.Json;

namespace SensitiveWords.Tests.Integration.TestHelpers
{
    public static class HttpResponseExtensions
    {
        public static async Task<T> ReadJsonAsync<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<T>();

            return result!;
        }
    }
}