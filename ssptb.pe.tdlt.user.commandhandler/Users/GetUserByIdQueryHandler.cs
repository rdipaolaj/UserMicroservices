﻿using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Queries;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data;
using ssptb.pe.tdlt.user.dto.Dtos;

namespace ssptb.pe.tdlt.user.commandhandler.Users;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<GetUserByIdResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<GetUserByIdQueryHandler> _logger;

    public GetUserByIdQueryHandler(ApplicationDbContext context, ILogger<GetUserByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResponse<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching user with ID {request.UserId}...");

        var user = await _context.Users.FindAsync(request.UserId);

        if (user == null)
        {
            return ApiResponseHelper.CreateErrorResponse<GetUserByIdResponse>("User not found", 404);
        }

        var response = user.Adapt<GetUserByIdResponse>();

        return ApiResponseHelper.CreateSuccessResponse(response, "User retrieved successfully");
    }
}