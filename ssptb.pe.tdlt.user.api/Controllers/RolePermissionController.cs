using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ssptb.pe.tdlt.user.api.Configuration;
using ssptb.pe.tdlt.user.command.Queries.RolePermission;

namespace ssptb.pe.tdlt.user.api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("ssptbpetdlt/user/api/v{v:apiVersion}/[controller]")]
public class RolePermissionController : CustomController
{
    private readonly ILogger<RolePermissionController> _logger;
    private readonly IMediator _mediator;

    public RolePermissionController(IMediator mediator, ILogger<RolePermissionController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Route("{rolId}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetRolePermissionByRoleId(Guid rolId)
    {
        _logger.LogInformation($"Fetching role permission with Role ID {rolId}...");
        var result = await _mediator.Send(new GetRolePermissionByRoleIdQuery(rolId));
        return OkorBadRequestValidationApiResponse(result);
    }
}
