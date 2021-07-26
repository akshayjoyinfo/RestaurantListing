using InHouse.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace InHouse.API.Filters
{
    [ExcludeFromCodeCoverage]
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Defines the _exceptionHandlers.
        /// </summary>
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiExceptionFilterAttribute"/> class.
        /// </summary>
        public ApiExceptionFilterAttribute()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(InHouseValidationException), HandleValidationException },
                { typeof(InHouseNotFoundException), HandleNotFoundException },
            };
        }

        /// <summary>
        /// The OnException.
        /// </summary>
        /// <param name="context">The context<see cref="ExceptionContext"/>.</param>
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        /// <summary>
        /// The HandleException.
        /// </summary>
        /// <param name="context">The context<see cref="ExceptionContext"/>.</param>
        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            HandleUnknownException(context);
        }

        /// <summary>
        /// The HandleUnknownException.
        /// </summary>
        /// <param name="context">The context<see cref="ExceptionContext"/>.</param>
        private void HandleUnknownException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Error Occured",
                Instance = context.HttpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            };

            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ApiExceptionFilterAttribute>>();
            var exceptionRequest = context.HttpContext.Request;
            logger.LogError(context.Exception, "Inhouse API Unhandled Error: {exceptionRequest}", exceptionRequest);

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// The HandleValidationException.
        /// </summary>
        /// <param name="context">The context<see cref="ExceptionContext"/>.</param>
        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as InHouseValidationException;

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// The HandleInvalidModelStateException.
        /// </summary>
        /// <param name="context">The context<see cref="ExceptionContext"/>.</param>
        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            var details = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// The HandleNotFoundException.
        /// </summary>
        /// <param name="context">The context<see cref="ExceptionContext"/>.</param>
        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as InHouseNotFoundException;

            var details = new ProblemDetails()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

    }
}
