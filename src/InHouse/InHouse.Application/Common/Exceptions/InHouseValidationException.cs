using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InHouse.Application.Common.Exceptions
{
    public class InHouseValidationException : Exception
    {
        public InHouseValidationException()
           : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public InHouseValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
