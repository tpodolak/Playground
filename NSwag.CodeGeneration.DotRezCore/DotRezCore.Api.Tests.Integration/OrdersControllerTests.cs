using System.Threading.Tasks;
using DotRezCore.Api.Tests.Integration.ApiClients.V1;
using DotRezCore.Api.Tests.Integration.Models.V1;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace DotRezCore.Api.Tests.Integration
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task GetMethodTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var httpClient = server.CreateClient();
            var apiClient = new DotRezCoreApiClient(httpClient, null);
            var result = await apiClient.ApiOrdersByIdGetAsync(1);

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetMethodByRequestObjectTest()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            var httpClient = server.CreateClient();
            var apiClient = new DotRezCoreApiClient(httpClient, null);
            var result = await apiClient.ApiOrdersGetAsync(new GetOrderRequest
            {
                Gender = Gender.Male,
                Id = "1",
                Type = "type"
            });

            result.Should().NotBeNull();
        }
    }
}