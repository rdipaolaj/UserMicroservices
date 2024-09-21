using MediatR;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data.Validations.Users.Service;
using ssptb.pe.tdlt.user.data.Validations.Users;
using ssptb.pe.tdlt.user.dto.Dtos;
using ssptb.pe.tdlt.user.redis.Users;
using ssptb.pe.tdlt.user.internalservices.Helpers;
using Microsoft.EntityFrameworkCore;
using ssptb.pe.tdlt.user.data.Validations.Role.Service;
using ssptb.pe.tdlt.user.data.Validations.Role;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserResponse>>
{
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserService _userService;
    private readonly IRedisUserService _redisUserService;
    private readonly IRoleValidationService _roleValidationService;
    private readonly ILogger<CreateUserCommandHandler> _logger;

    public CreateUserCommandHandler(
        IUserValidationService userValidationService,
        IPasswordHasher passwordHasher,
        IUserService userService,
        IRedisUserService redisUserService,
        IRoleValidationService roleValidationService,
        ILogger<CreateUserCommandHandler> logger)
    {
        _userValidationService = userValidationService;
        _passwordHasher = passwordHasher;
        _userService = userService;
        _redisUserService = redisUserService;
        _roleValidationService = roleValidationService;
        _logger = logger;
    }

    public async Task<ApiResponse<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando proceso de creación de usuario...");

        var validationResult = await _userValidationService.ValidateUserAsync(request, cancellationToken);

        if (!validationResult.Success)
        {
            _logger.LogWarning("Error en la validación del usuario: {Errors}", validationResult.Errors);
            return ApiResponseHelper.CreateErrorResponse<CreateUserResponse>("User validation failed", 400, validationResult.Errors.ToList());
        }

        // Validar el rol del usuario
        var roleValidationResult = await _roleValidationService.ValidateRoleAsync(request.UserRole, cancellationToken);
        if (!roleValidationResult.Success)
        {
            _logger.LogWarning("Error en la validación del rol: {Errors}", roleValidationResult.Errors);
            return ApiResponseHelper.CreateErrorResponse<CreateUserResponse>("Role validation failed", 400, roleValidationResult.Errors);
        }

        var (hashedPassword, salt) = _passwordHasher.HashPassword(request.HashedPassword);

        // Crear el usuario
        var user = await _userService.CreateUserAsync(request, salt, hashedPassword, roleValidationResult.Data, cancellationToken);

        // Sincronizar con Redis
        await _redisUserService.SyncUserWithRedisAsync(user, request.Username);

        _logger.LogInformation("Usuario creado y guardado en Redis correctamente.");

        var response = new CreateUserResponse
        {
            UserId = user.UserId
        };

        return ApiResponseHelper.CreateSuccessResponse(response, "User created successfully");
    }

}
