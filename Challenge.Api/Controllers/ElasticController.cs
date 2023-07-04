using Challenge.Application.Permissions.Commands;
using Challenge.Application.Permissions.Queries;
using Challenge.Application.Permissions.Services.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElasticController : ControllerBase
    {
        private readonly ILogger<ElasticController> _logger;
        private IMediator _mediator;

        public ElasticController(ILogger<ElasticController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        
        [HttpGet(Name = "GetPermissionsWithElastic")]
        public async Task<ActionResult<IEnumerable<EmployeePermissionTypeDto>>> GetElastic([FromQuery] GetAllPermissionElasticQuery input)
        {
            // -- Call Handler of GetAllPermissionQueryHandler
            var result = await _mediator.Send(input);

            return Ok(result);

        }
    }
}