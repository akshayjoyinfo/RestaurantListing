using InHouse.API.Contracts.Resturant.Listing.V1.Enums;
using InHouse.API.Contracts.Resturant.Listing.V1.Response;
using InHouse.Application.Extensions;
using InHouse.Application.Restaurant.Listing.V1.Commands;
using InHouse.Application.Restaurant.Listing.V1.Mappings;
using InHouse.Application.Restaurant.Listing.V1.Usecases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InHouse.Application.Restaurant.Listing.V1.Usecases.Services
{
    public class TransformWorkingHoursService : ITransformWorkingHoursService
    {
        public TransformWorkingHoursService()
        {

        }
        /// <summary>
        /// 1. Sort Working Schedules by Monday -> Sunday according Problem Statement
        /// to make sure index are correct when you move upward
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RestaurantListingReadableWorkingHoursResponse> GenerateHumanReadableWorkingHours(RestaurantWorkingHoursPayload request)
        {
            await Task.CompletedTask;
            var output = new RestaurantListingReadableWorkingHoursResponse();

            var sortedWorkingDaySchedules = request.WorkingDaySchedules.OrderBy(x => ((int)x.DayOfWeek + 6) % 7)
                                .ToList();
            
            List<string> outputTimings = new List<string>();


            for (var current = 0; current < sortedWorkingDaySchedules.Count; ++current)
            {
                if (sortedWorkingDaySchedules[current].Schedules.Any())
                {
                    if (current == 0 || current != sortedWorkingDaySchedules.Count - 1)
                    {
                        outputTimings = PopulateReadableWorkingHours(sortedWorkingDaySchedules[current], sortedWorkingDaySchedules[current + 1]);
                        output.WorkingDayReadableSchedules
                            .Add(new WorkingDayReadableSchedule()
                            {
                                DayOfWeek = sortedWorkingDaySchedules[current].DayOfWeek,
                                ReadableSchedules = outputTimings
                            });
                    }
                    else if (current == request.WorkingDaySchedules.Count - 1)
                    {
                        outputTimings = PopulateReadableWorkingHours(sortedWorkingDaySchedules[current], sortedWorkingDaySchedules[0]);
                        output.WorkingDayReadableSchedules
                            .Add(new WorkingDayReadableSchedule()
                            {
                                DayOfWeek = sortedWorkingDaySchedules[current].DayOfWeek,
                                ReadableSchedules = outputTimings
                            });
                    }
                }
                else
                {
                    output.WorkingDayReadableSchedules
                            .Add(new WorkingDayReadableSchedule()
                            {
                                DayOfWeek = sortedWorkingDaySchedules[current].DayOfWeek,
                                ReadableSchedules = new List<string>()
                                {
                                    nameof(ReadableWorkingDayType.Closed)
                                }
                            });
                }
            }


            return output;
        }

        private List<string> PopulateReadableWorkingHours(WorkingDaySchedule currentDay, WorkingDaySchedule nextDay)
        {
            var isOpen = false;
            var outputs = new List<string>();
            var openTime = string.Empty;
            var openDay = string.Empty;
            var closingTime = string.Empty;
            var closingDay = string.Empty;
            
            foreach(var workingDay in currentDay.Schedules.OrderBy(x=> x.Value))
            {
                if(workingDay.Type == WorkingDayType.open)
                {
                    openTime = workingDay.Value.Get12HourClockTime();
                    isOpen = true;
                }
                else
                {
                    if(isOpen==true)
                    {
                        closingTime = workingDay.Value.Get12HourClockTime();
                        outputs.Add($"{openTime} - {closingTime}");
                        isOpen = false;
                    }
                    
                }
            }

            if(isOpen == true)
            {
                var nextDayClosingTime = nextDay.Schedules.OrderBy(x => x.Value).FirstOrDefault()?.Value.Get12HourClockTime();
                outputs.Add($"{openTime} - {nextDayClosingTime}");
            }
            return outputs;
        }
    }
}
