using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InHouse.API.Contracts.Resturant.Listing.V1.Response
{
    public class RestaurantListingReadableWorkingHoursResponse
    {
        public List<WorkingDayReadableSchedule> WorkingDayReadableSchedules { get; set; }

        public RestaurantListingReadableWorkingHoursResponse()
        {
            WorkingDayReadableSchedules = new List<WorkingDayReadableSchedule>();
        }
    }

    public class WorkingDayReadableSchedule
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<string> ReadableSchedules { get; set; }
    }
}
