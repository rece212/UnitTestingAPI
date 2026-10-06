using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using UnitTestingAPI;
using UnitTestingAPI.Models;

namespace UnitTestingAPi.Testing
{
    public class ItemsControllerTests : IClassFixture
        <WebApplicationFactory<UnitTestingAPI.Program>>
    {
        private readonly HttpClient _client;
        public ItemsControllerTests
            (WebApplicationFactory<UnitTestingAPI.Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetAll_ReturnsOkStatus_AndListOfItems()
        {
            //Act
            var response = await _client.GetAsync("/api/items");

            //Assert using built-in Xunit Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadFromJsonAsync<List<string>>();
            Assert.NotNull(content);
            Assert.Contains("Laptop", content);
        }
        [Fact]
        public async Task GetById_WithValidIndex_ReturnsOk()
        {
            var responce = await _client.GetAsync("/api/items/0");
            Assert.Equal(HttpStatusCode.OK, responce.StatusCode);
        }
        [Fact]
        public async Task GetById_WithInvalidIndex_ReturnsNotFound()
        {
            var responce = await _client.GetAsync("/api/items/999");
            Assert.Equal(HttpStatusCode.NotFound, responce.StatusCode);
        }
        [Fact]
        public async Task Create_WithValidData_ReturnsCreated()
        {

            var request = new CreateItemRequest { Name = "Monitor" };

            var response = await _client.PostAsJsonAsync("/api/items", request);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Create_WithEmptyName_ReturnsBadRequest()
        {
            var request = new CreateItemRequest { Name = "" };
            var response = await _client.PostAsJsonAsync("/api/items", request);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


    }
}
