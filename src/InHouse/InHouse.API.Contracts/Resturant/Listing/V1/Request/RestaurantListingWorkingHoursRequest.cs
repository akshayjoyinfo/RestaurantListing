using InHouse.API.Contracts.Resturant.Listing.V1.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace InHouse.API.Contracts.Resturant.Listing.V1.Request
{

    /// <summary>
    /// Problem statement mentioned that key as one of the domain value for the Date
    /// it could have been dayOfWeek : Monday
    /// will optimize this in V2 Version, since problem statement is given 
    /// we are sticking with the below contract
    /// </summary>
    public class RestaurantListingWorkingHoursRequest
    {
        [JsonProperty("monday")]
        public List<WorkingDay> Monday { get; set; }

        [JsonProperty("tuesday")]
        public List<WorkingDay> Tuesday { get; set; }

        [JsonProperty("wednesday")]
        public List<WorkingDay> Wednesday { get; set; }

        [JsonProperty("thursday")]
        public List<WorkingDay> Thursday { get; set; }

        [JsonProperty("friday")]
        public List<WorkingDay> Friday { get; set; }

        [JsonProperty("saturday")]
        public List<WorkingDay> Saturday { get; set; }

        [JsonProperty("sunday")]
        public List<WorkingDay> Sunday { get; set; }
    }

    public class WorkingDay
    {
        [JsonProperty("type")]
        public WorkingDayType Type { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
