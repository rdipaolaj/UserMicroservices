using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApiResponse<UpdateUserResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UpdateUserCommandHandler> _logger;

    public UpdateUserCommandHandler(ApplicationDbContext context, ILogger<UpdateUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating user with ID {request.UserId}...");

        var user = await _context.Users.FindAsync(request.UserId);

        if (user is null)
        {
            return ApiResponseHelper.CreateErrorResponse<UpdateUserResponse>("User not found", 404);
        }

        // Usar Mapster para mapear las propiedades del comando al usuario
        request.Adapt(user);

        await _context.SaveChangesAsync(cancellationToken);

        var response = new UpdateUserResponse
        {
            UserId = user.UserId,
            Message = "User updated successfully"
        };

        return ApiResponseHelper.CreateSuccessResponse(response);
    }
}
