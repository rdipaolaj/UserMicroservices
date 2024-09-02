using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Queries;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class GetUserByUsernameQueryHandler : IRequestHandler<GetUserByUsernameQuery, ApiResponse<GetUserByUsernameResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<GetUserByUsernameQueryHandler> _logger;

    public GetUserByUsernameQueryHandler(ApplicationDbContext context, ILogger<GetUserByUsernameQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<GetUserByUsernameResponse>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching user with username {request.Username}...");

        var user = await _context.Users
                             .SingleOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

        if (user == null)
        {
            return ApiResponseHelper.CreateErrorResponse<GetUserByUsernameResponse>("User not found", 404);
        }

        var response = user.Adapt<GetUserByUsernameResponse>();

        return ApiResponseHelper.CreateSuccessResponse(response, "User retrieved successfully");
    }
}
