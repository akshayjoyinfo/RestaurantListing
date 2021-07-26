using AutoMapper;
using InHouse.API.Contracts.Resturant.Listing.V1.Request;
using InHouse.Application.Common.Mappings;
using InHouse.Application.Restaurant.Listing.V1.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InHouse.Application.Restaurant.Listing.V1.Mappings
{
    public class RestaurantWorkingHoursPayload : IMapFrom<ConvertRestaurantListingWorkingHoursCommand>
    {

        public RestaurantWorkingHoursPayload()
        {
            WorkingDaySchedules = new List<WorkingDaySchedule>();
        }
        public List<WorkingDaySchedule> WorkingDaySchedules { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ConvertRestaurantListingWorkingHoursCommand, RestaurantWorkingHoursPayload>()
                .ForMember(dest => dest.WorkingDaySchedules, src => src.MapFrom(opt=> GenerateWorkingSchedules(opt)));
        }

        private List<WorkingDaySchedule> GenerateWorkingSchedules(ConvertRestaurantListingWorkingHoursCommand opt)
        {
            List<WorkingDaySchedule> workingDaySchedules = new List<WorkingDaySchedule>();

            if (opt.Sunday != null)
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Sunday,
                    Schedules = opt.Sunday
                });

            if (opt.Monday != null)
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Monday,
                    Schedules = opt.Monday
                });

            if (opt.Tuesday != null )
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    Schedules = opt.Tuesday
                });

            if (opt.Wednesday != null )
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    Schedules = opt.Wednesday
                });

            if (opt.Thursday != null )
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Thursday,
                    Schedules = opt.Thursday
                });

            if (opt.Friday != null )
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Friday,
                    Schedules = opt.Friday
                });

            if (opt.Saturday != null )
                workingDaySchedules.Add(new WorkingDaySchedule()
                {
                    DayOfWeek = DayOfWeek.Saturday,
                    Schedules = opt.Saturday
                });

            return workingDaySchedules;
        }
    }

    public class WorkingDaySchedule
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<WorkingDay> Schedules { get; set; }
    }
}
