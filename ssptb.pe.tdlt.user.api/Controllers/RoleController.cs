using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ssptb.pe.tdlt.user.api.Configuration;
using ssptb.pe.tdlt.user.command.Queries;

namespace ssptb.pe.tdlt.user.api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("ssptbpetdlt/user/api/v{v:apiVersion}/[controller]")]
public class RoleController : CustomController
{
    private readonly ILogger<RoleController> _logger;
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator, ILogger<RoleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Route("{rolId}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetRoleByRoleId(Guid rolId)
    {
        _logger.LogInformation($"Fetching role with ID {rolId}...");
        var result = await _mediator.Send(new GetUserByIdQuery(rolId));
        return OkorBadRequestValidationApiResponse(result);
    }
}
