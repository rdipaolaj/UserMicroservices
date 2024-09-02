using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ssptb.pe.tdlt.user.api.Configuration;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.command.Queries;
using ssptb.pe.tdlt.user.common.Responses;

namespace ssptb.pe.tdlt.user.api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("ssptbpetdlt/user/api/v{v:apiVersion}/[controller]")]
public class UserController : CustomController
{
    private readonly ILogger<UserController> _logger;
    private readonly IMediator _mediator;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [Route("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        _logger.LogInformation("Creating user...");
        var result = await _mediator.Send(command);
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpGet]
    [Route("{id}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        _logger.LogInformation($"Fetching user with ID {id}...");
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpGet]
    [Route("username/{username}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        _logger.LogInformation($"Fetching user with Username {username}...");
        var result = await _mediator.Send(new GetUserByUsernameQuery(username));
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpGet]
    [Route("GetUsers")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
    {
        _logger.LogInformation("Fetching users...");
        var result = await _mediator.Send(query);
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpPut]
    [Route("{id}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
    {
        _logger.LogInformation($"Updating user with ID {id}...");
        command.UserId = id;
        var result = await _mediator.Send(command);
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpDelete]
    [Route("{id}")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        _logger.LogInformation($"Deleting user with ID {id}...");
        var result = await _mediator.Send(new DeleteUserCommand(id));
        return OkorBadRequestValidationApiResponse(result);
    }
}
