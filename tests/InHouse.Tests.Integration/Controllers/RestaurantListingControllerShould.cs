using FluentAssertions;
using InHouse.API;
using InHouse.API.Contracts.V1;
using InHouse.Application.Restaurant.Listing.V1.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InHouse.Tests.Integration.Controllers
{
    [ExcludeFromCodeCoverage]
    [Collection("RestaurantListingCollectionFixture")]
    public class RestaurantListingControllerShould
    {
        private readonly HttpClient _client;
        private readonly InHouseApiFactory<Startup> _factory;
        private static readonly string _jsonPath = "Jsons" + Path.DirectorySeparatorChar;
        private static readonly string _responsePath = "Responses" + Path.DirectorySeparatorChar;

        public RestaurantListingControllerShould(InHouseApiFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Theory]
        [InlineData("ResturantListing.json", "ResturantListing_Response.txt")]
        [InlineData("ResaurantListing_Usecase2.json", "ResaurantListing_Usecase2_Response.txt")]
        [InlineData("ResaurantListing_Multiple_Hours_Usecase2.json", "ResaurantListing_Multiple_Hours_Usecase2_Response.txt")]
        public async Task GetHumanReadableWorkingHoursFormUsecases(string requestFile, string responseFile)
        {
            var inputData = await Utilities.GetRequestObjectFromJsonFile<ConvertRestaurantListingWorkingHoursCommand>(requestFile, _jsonPath);
            var apiContent = Utilities.GetRequestContent(inputData);

            var response = await _client.PostAsync(ApiRoutesV1.RestaurantListing.GetWorkingHours, apiContent);
            response.EnsureSuccessStatusCode();

            var responseVm = await Utilities.GetResponseStringContent(response);
            var expected = await Utilities.GetFileContent(responseFile, _responsePath);

            expected.Should().Be(responseVm);
        }

        [Theory]
        [InlineData("ResturantListing_Invalid.json")]
        public async Task GetBadRequestForInvalidInput(string requestFile)
        {
            var inputData = await Utilities.GetRequestObjectFromJsonFile<ConvertRestaurantListingWorkingHoursCommand>(requestFile, _jsonPath);
            var apiContent = Utilities.GetRequestContent(inputData);

            var response = await _client.PostAsync(ApiRoutesV1.RestaurantListing.GetWorkingHours, apiContent);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
