using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Queries;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data;
using ssptb.pe.tdlt.user.dto.Dtos;
using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ApiResponse<List<GetUserByIdResponse>>>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<GetUsersQueryHandler> _logger;

    public GetUsersQueryHandler(ApplicationDbContext context, ILogger<GetUsersQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<List<GetUserByIdResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching users...");

        List<User> users = await _context.Users.ToListAsync(cancellationToken);

        if (users is null || !users.Any())
        {
            return ApiResponseHelper.CreateErrorResponse<List<GetUserByIdResponse>>("No users found", 404);
        }

        var response = users.Adapt<List<GetUserByIdResponse>>();

        return ApiResponseHelper.CreateSuccessResponse(response, "Users retrieved successfully");
    }
}