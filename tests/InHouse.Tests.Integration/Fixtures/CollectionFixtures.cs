using InHouse.API;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InHouse.Tests.Integration.Fixtures
{
    [CollectionDefinition("HealthCheckCollectionFixture")]
    public class HealthCheckCollectionFixture : ICollectionFixture<InHouseApiFactory<Startup>>
    {

    }

    [CollectionDefinition("RestaurantListingCollectionFixture")]
    public class RestaurantListingCollectionFixture : ICollectionFixture<InHouseApiFactory<Startup>>
    {

    }
}
