using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InHouse.Application.Common.Behaviours
{
    [ExcludeFromCodeCoverage]
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<TRequest> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnhandledExceptionBehaviour{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{TRequest}"/>.</param>
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
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
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogError(ex, "EmM API : Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

                throw;
            }
        }
    }
}
