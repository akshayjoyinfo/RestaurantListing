using InHouse.API.Contracts.Resturant.Listing.V1.Response;
using InHouse.Application.Restaurant.Listing.V1.Commands;
using InHouse.Application.Restaurant.Listing.V1.Mappings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InHouse.Application.Restaurant.Listing.V1.Usecases.Interfaces
{
    public interface ITransformWorkingHoursService
    {
        Task<RestaurantListingReadableWorkingHoursResponse> GenerateHumanReadableWorkingHours(RestaurantWorkingHoursPayload request);
    }
}
