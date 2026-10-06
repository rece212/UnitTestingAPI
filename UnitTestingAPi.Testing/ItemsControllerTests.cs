using Microsoft.AspNetCore.Mvc.Testing;
using UnitTestingAPI;

namespace UnitTestingAPi.Testing
{
    public class ItemsControllerTests:IClassFixture
        <WebApplicationFactory<UnitTestingAPI.Program>>
    {
        private readonly HttpClient _client;
        public ItemsControllerTests
            (WebApplicationFactory<UnitTestingAPI.Program> factory)
        {
            _client =factory.CreateClient();
        }
        [Fact]
        public void Test1()
        {

        }
    }
}
