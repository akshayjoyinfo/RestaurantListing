using FluentValidation;
using InHouse.API.Contracts.Resturant.Listing.V1.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InHouse.Application.Restaurant.Listing.V1.Commands
{
    public class ConvertRestaurantListingWorkingHoursCommandValidator : AbstractValidator<ConvertRestaurantListingWorkingHoursCommand>
    {
        public ConvertRestaurantListingWorkingHoursCommandValidator()
        {
            When(cb => cb.Sunday != null && cb.Sunday.Any(), () =>
                {
                    RuleFor(cb => ValidateWorkingDay(cb.Sunday)).Must(x=> x).WithName(cb=> "Sunday").WithMessage("Should have valid Unix Time Epochs on Sunday , should be between 0-86399");
                });

            When(cb => cb.Monday != null && cb.Monday.Any(), () =>
            {
                RuleFor(cb => ValidateWorkingDay(cb.Monday)).Must(x => x).WithName(cb => "Monday").WithMessage("Should have valid Unix Time Epochs on Monday, should be between 0-86399");
            });

            When(cb => cb.Tuesday != null && cb.Tuesday.Any(), () =>
            {
                RuleFor(cb => ValidateWorkingDay(cb.Tuesday)).Must(x => x).WithName(cb => "Tuesday").WithMessage("Should have valid Unix Time Epochs on Tuesday , should be between 0-86399");
            });

            When(cb => cb.Wednesday != null && cb.Wednesday.Any(), () =>
            {
                RuleFor(cb => ValidateWorkingDay(cb.Wednesday)).Must(x => x).WithName(cb => "Wednesday").WithMessage("Should have valid Unix Time Epochs on Wednesday , should be between 0-86399");
            });

            When(cb => cb.Thursday != null && cb.Thursday.Any(), () =>
            {
                RuleFor(cb => ValidateWorkingDay(cb.Thursday)).Must(x => x).WithName(cb => "Thursday").WithMessage("Should have valid Unix Time Epochs on Thursday , should be between 0-86399");
            });

            When(cb => cb.Friday != null && cb.Friday.Any(), () =>
            {
                RuleFor(cb => ValidateWorkingDay(cb.Friday)).Must(x => x).WithName(cb => "Friday").WithMessage("Should have valid Unix Time Epochs on Friday , should be between 0-86399");
            });

            When(cb => cb.Saturday != null && cb.Saturday.Any(), () =>
            {
                RuleFor(cb => ValidateWorkingDay(cb.Saturday)).Must(x => x).WithName(cb => "Saturday").WithMessage("Should have valid Unix Time Epochs on Saturday , should be between 0-86399");
            });


        }

        private bool ValidateWorkingDay(List<WorkingDay> day)
        {
            var valueChecks = day.Count(x => x.Value < 0 || x.Value > 86399);
            if (valueChecks > 0)
                return false;

            else return true;
        }
    }
}
