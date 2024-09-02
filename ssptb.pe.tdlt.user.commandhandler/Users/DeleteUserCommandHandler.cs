using MediatR;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<string>>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(ApplicationDbContext context, ILogger<DeleteUserCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting user with ID {request.UserId}...");

        var user = await _context.Users.FindAsync(request.UserId);

        if (user is null)
        {
            return ApiResponseHelper.CreateErrorResponse("User not found", 404);
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return ApiResponseHelper.CreateSuccessResponse("User deleted successfully");
    }
}
