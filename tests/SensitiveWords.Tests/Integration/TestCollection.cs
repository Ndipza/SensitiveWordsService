namespace SensitiveWords.Tests.Integration
{
    using Xunit;

    namespace SensitiveWords.Tests.Integration
    {
        [CollectionDefinition("ApiCollection")]
        public class TestCollection : ICollectionFixture<CustomWebApplicationFactory>
        {
            // This class has no code.
            // Its purpose is to apply the collection definition
            // and share CustomWebApplicationFactory across tests.
        }
    }
}
