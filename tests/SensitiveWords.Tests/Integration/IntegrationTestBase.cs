namespace SensitiveWords.Tests.Integration
{
    [Collection("ApiCollection")]
    public abstract class IntegrationTestBase
    {
        protected readonly HttpClient Client;

        protected IntegrationTestBase(CustomWebApplicationFactory factory)
        {
            Client = factory.CreateClient();
        }
    }
}