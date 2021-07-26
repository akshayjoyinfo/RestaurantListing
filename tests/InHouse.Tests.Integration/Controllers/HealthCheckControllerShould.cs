using FluentAssertions;
using InHouse.API;
using InHouse.API.Contracts.HealthCheck.Response;
using InHouse.API.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InHouse.Tests.Integration.Controllers
{
    [ExcludeFromCodeCoverage]
    [Collection("HealthCheckCollectionFixture")]
    public class HealthCheckControllerShould
    {
        private readonly HttpClient _client;
        private readonly InHouseApiFactory<Startup> _factory;

        public HealthCheckControllerShould(InHouseApiFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task GetHealthyResponse()
        {
            var response = await _client.GetAsync(ApiRoutesV1.HealthCheck.Get);
            response.EnsureSuccessStatusCode();
            var responseVm = await Utilities.GetResponseContent<HealthCheckResponse>(response);

            responseVm?.Status.Should().Be("Healthy");
            responseVm?.Messsage.Should().BeNull();
        }

        [Fact]
        public async Task Get404Response()
        {
            var response = await _client.GetAsync(ApiRoutesV1.HealthCheck.Get+"someUrl");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
