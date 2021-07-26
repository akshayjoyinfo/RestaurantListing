using System;
using System.Collections.Generic;
using System.Text;

namespace InHouse.API.Contracts.V1
{
    public static class ApiRoutesV1
    {
        private const string Root = "";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;
        public const string HealthCheckTag = "Health";
        public const string RestaurantsBase = Base + "/restaurants";

        public static class HealthCheck
        {
            private const string HealthCheckBase = "/health";

            public const string Get = HealthCheckBase;

            public const string SwaggerSummary = "HealthCheck Endpoint";
            public const string SwaggerDescription = "Check the health check endpoiint for InHouse Restaurant Listing API";
        }

        public static class RestaurantListing
        {
            private const string RestaurantListingBase = RestaurantsBase + "/listings";

            public const string GetWorkingHours = RestaurantListingBase;
            public const string SwaggerSummary = "Convert Restaurant Working Schedule to Human Readable Format";
            public const string SwaggerDescription = @"

                Input JSON consists of keys indicating days of a week and corresponding
                opening hours as values.
                One JSON file includes data for one restaurant.
                {
                <dayofweek>: <opening hours>
                <dayofweek>: <opening hours>
                ...
                }
                <dayofweek> : monday / tuesday / wednesday / thursday / friday / saturday /
                sunday <opening hours>: an array of objects containing opening hours. Each
                object consists of two keys:
                type: open or close
                value: opening / closing time as UNIX time (1.1.1970 as a date),
                e.g., 32400 = 9 AM, 37800 = 10.30 AM,
                max value is 86399 = 11.59:59 PM
                Example: on Mondays a restaurant is open from 9 AM to 8 PM
                
                Output example in 12-hour clock format:
                Monday: 8 AM - 10 AM, 11 AM - 6 PM
                Tuesday: Closed
                Wednesday: 11 AM - 6 PM
                Thursday: 11 AM - 6 PM
                Friday: 11 AM - 9 PM
                Saturday: 11 AM - 9 PM
                Sunday: Closed
                
                Special cases
                • If a restaurant is closed the whole day, an array of opening hours is
                empty. o “Tuesday”: [] means a restaurant is closed on Tuesdays
                • A restaurant can be opened and closed multiple times during the same
                day, o E.g., on Mondays from 9 AM - 11 AM and from 1 PM to 5 PM
                • A restaurant might not be closed during the same day
                    o E.g., a restaurant is opened on a Sunday evening and closed early on
                    Monday morning. In that case Sunday- object includes only the opening
                    time.
                    o Closing time is part of the Monday -object.
                • When printing opening hours which span between multiple days, closing time is
                always a part of the day when a restaurant was opened (e.g., Sunday 8 PM -1
                AM)

            ";

            public const string RestaurantListingTag = "Restaurant Listings";
        }
    }
}
