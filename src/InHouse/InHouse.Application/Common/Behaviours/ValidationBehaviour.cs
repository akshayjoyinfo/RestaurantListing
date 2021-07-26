using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InHouse.Application.Common.Behaviours
{
    [ExcludeFromCodeCoverage]
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Defines the _validators.
        /// </summary>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;

        private readonly IHttpContextAccessor _httpContextAccessor;

        /// Initializes a new instance of the <see cref="ValidationBehaviour{TRequest, TResponse}"/> class.
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IHttpContextAccessor httpContextAccessor)
        {
            _validators = validators;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="request">The request<see cref="TRequest"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <param name="next">The next<see cref="RequestHandlerDelegate{TResponse}"/>.</param>
        /// <returns>The <see cref="Task{TResponse}"/>.</returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var requestName = typeof(TRequest).Name;
                    //var exception = new ValidationException(failures);
                    var method = _httpContextAccessor.HttpContext?.Request.Method;
                    var path = _httpContextAccessor.HttpContext?.Request.Path;
                    var exception = new Exceptions.InHouseValidationException(failures);
                    _logger.LogWarning(exception, "InHouse API : Request: Validation Exception for Request {requestName} {request} {failures}", requestName, request, failures);
                    throw exception;
                }
            }
            return await next();
        }
    }
}
