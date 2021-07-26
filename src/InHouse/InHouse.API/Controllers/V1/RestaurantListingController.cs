using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InHouse.API.Contracts.HealthCheck.Response;
using InHouse.API.Contracts.Resturant.Listing.V1.Request;
using InHouse.API.Contracts.Resturant.Listing.V1.Response;
using InHouse.API.Contracts.V1;
using InHouse.Application.Restaurant.Listing.V1.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InHouse.API.Controllers.V1
{
    
    public class RestaurantListingController : BaseApiController
    {
        [HttpPost(ApiRoutesV1.RestaurantListing.GetWorkingHours)]
        [SwaggerOperation(Summary = ApiRoutesV1.RestaurantListing.SwaggerSummary,
            Tags = new[]
            { ApiRoutesV1.RestaurantListing.RestaurantListingTag },
            Description = ApiRoutesV1.RestaurantListing.SwaggerDescription)]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<string>> GetHumanReadableWorkingHours([FromBody] ConvertRestaurantListingWorkingHoursCommand request)
        {
         
            var response = await Mediator.Send(request);

            return Ok(response);
        }

    }
}
