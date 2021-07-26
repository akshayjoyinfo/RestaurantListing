using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InHouse.API.Contracts.HealthCheck.Response;
using InHouse.API.Contracts.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace InHouse.API.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        [HttpGet(ApiRoutesV1.HealthCheck.Get)]
        [SwaggerOperation(Summary = ApiRoutesV1.HealthCheck.SwaggerSummary, Tags = new[]
            { ApiRoutesV1.HealthCheckTag },
            Description = ApiRoutesV1.HealthCheck.SwaggerDescription)]
        [SwaggerResponse(200, Type = typeof(HealthCheckResponse))]
        public ActionResult<HealthCheckResponse> Get()
        {
            // this is to test Logs are working as expected
            _logger.LogInformation("Called HealthCheck endpoint");
            return Ok(new HealthCheckResponse()
            {
                Status = "Healthy",
            });
        }
    }
}
