using MediatR;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data.Validations.Users.Service;
using ssptb.pe.tdlt.user.data.Validations.Users;
using ssptb.pe.tdlt.user.dto.Dtos;
using ssptb.pe.tdlt.user.redis.Users;
using ssptb.pe.tdlt.user.internalservices.Helpers;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserResponse>>
{
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;
    private readonly IRedisUserService _redisUserService;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IUserValidationService userValidationService,
        IPasswordHasher passwordHasher,
        IUserService userService,
        IRedisUserService redisUserService,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userValidationService = userValidationService;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _redisUserService = redisUserService;
        _logger = logger;
    }

    public async Task<ApiResponse<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de usuario...");

        await _userValidationService.ValidateUserAsync(request, cancellationToken);

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.HashedPassword);

        var user = await _userService.CreateUserAsync(request, salt, hashedPassword, cancellationToken);

        await _redisUserService.SyncUserWithRedisAsync(user, request.Username);

        _logger.LogInformation("Usuario creado y guardado en Redis correctamente.");

        var response = new CreateUserResponse
        {
            UserId = user.UserId
        };

        return ApiResponseHelper.CreateSuccessResponse(response, "User created successfully");
    }

}
