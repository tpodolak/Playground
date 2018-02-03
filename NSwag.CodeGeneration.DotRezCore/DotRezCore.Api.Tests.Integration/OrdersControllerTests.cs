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
            using (var client = IntegrationTestsFixture.Server.CreateClient())
            {
                var apiClient = new OrdersClient(client, null);
                var result = await apiClient.ByIdGetAsync(1);

                result.Should().NotBeNull();
            }
        }

        [Fact]
        public async Task GetMethodByRequestObjectTest()
        {
            using (var client = IntegrationTestsFixture.Server.CreateClient())
            {
                var apiClient = new OrdersClient(client, null);
                var result = await apiClient.GetAsync(new GetOrderRequest
                {
                    Gender = Gender.Male,
                    Id = "1",
                    Type = "type"
                });

                result.Should().NotBeNull();
            }
        }
    }
}