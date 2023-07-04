using Challenge.Application.Permissions.Commands;
using Challenge.Application.Permissions.Queries;
using Challenge.Application.Permissions.Services.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Challenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private IMediator _mediator;

        public PermissionController(ILogger<PermissionController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost(Name = "RequestPermission")]
        public async Task<ActionResult<long>> Post(RequestPermissionCommand input)
        {
            // -- Call Handler of RequestPermissionCommandHandler
            var result = await _mediator.Send(input);

            return Ok(result);
        }

        [HttpPut("{id}",Name = "ModifyPermission")]
        public async Task<ActionResult<long>> Put(long id, ModifyPermissionCommand input)
        {
            // -- Call Handler of ModifyPermissionCommandHandler
            var result = await _mediator.Send(input);

            return Ok(result);
        }

        [HttpGet(Name = "GetPermissions")]
        public async Task<ActionResult<IEnumerable<EmployeePermissionTypeDto>>> Get([FromQuery]GetAllPermissionQuery input)
        {
            // -- Call Handler of GetAllPermissionQueryHandler
            var result = await _mediator.Send(input);

            return Ok(result);

        }
    }
}