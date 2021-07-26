using AutoMapper;
using InHouse.API.Contracts.Resturant.Listing.V1.Enums;
using InHouse.API.Contracts.Resturant.Listing.V1.Request;
using InHouse.API.Contracts.Resturant.Listing.V1.Response;
using InHouse.Application.Extensions;
using InHouse.Application.Restaurant.Listing.V1.Mappings;
using InHouse.Application.Restaurant.Listing.V1.Usecases.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InHouse.Application.Restaurant.Listing.V1.Commands
{
   public class ConvertRestaurantListingWorkingHoursCommand : RestaurantListingWorkingHoursRequest,
        IRequest<string>
    {

    }

    public class ConvertRestaurantListingWorkingHoursCommandHandler :
        IRequestHandler<ConvertRestaurantListingWorkingHoursCommand, string>
    {

        private readonly ITransformWorkingHoursService _service;
        private readonly IMapper _mapper;

        public ConvertRestaurantListingWorkingHoursCommandHandler(ITransformWorkingHoursService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<string> Handle(ConvertRestaurantListingWorkingHoursCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            // transform the input to better fromats to process easier

            var requestPayload = _mapper.Map<ConvertRestaurantListingWorkingHoursCommand, RestaurantWorkingHoursPayload>(request);

            var readableOutput = await _service.GenerateHumanReadableWorkingHours(requestPayload);

            var formattedOutput = GenerateStringContent(readableOutput);

            return formattedOutput;
        }

        private string GenerateStringContent(RestaurantListingReadableWorkingHoursResponse readableOutput)
        {
            var stringBuilder = new StringBuilder();
            foreach(var item in readableOutput.WorkingDayReadableSchedules)
            {
                if (item.ReadableSchedules.Any())
                {
                    stringBuilder.AppendLine($"{ item.DayOfWeek}: {string.Join(", ", item.ReadableSchedules)}");
                }
                else
                {
                    // this is for special case
                    // when Sunday opens at 10 PM and closed at next Monday morning, remaining day restaurant was closed
                    // this happens, accroding to rule Closed time should be there for Sunday but Monday has nothing 
                    // so that reason I set it as closed
                    stringBuilder.AppendLine($"{ item.DayOfWeek}: Closed");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
