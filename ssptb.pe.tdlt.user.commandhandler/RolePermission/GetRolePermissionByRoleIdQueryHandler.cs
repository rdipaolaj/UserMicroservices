using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using ssptb.pe.tdlt.user.command.Queries.RolePermission;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data.Validations.RolePermission.Service;
using ssptb.pe.tdlt.user.dto.Dtos.RolePermission;

namespace ssptb.pe.tdlt.user.commandhandler.RolePermission;
public class GetRolePermissionByRoleIdQueryHandler : IRequestHandler<GetRolePermissionByRoleIdQuery, ApiResponse<GetRolePermissionByRoleIdResponse>>
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly ILogger<GetRolePermissionByRoleIdQueryHandler> _logger;

    public GetRolePermissionByRoleIdQueryHandler(IRolePermissionService rolePermissionService, ILogger<GetRolePermissionByRoleIdQueryHandler> logger)
    {
        _rolePermissionService = rolePermissionService;
        _logger = logger;
    }

    public async Task<ApiResponse<GetRolePermissionByRoleIdResponse>> Handle(GetRolePermissionByRoleIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Fetching permissions for Role ID {request.RoleId}");

        var permissions = await _rolePermissionService.GetPermissionsByRoleIdAsync(request.RoleId);

        if (permissions == null || !permissions.Any())
        {
            _logger.LogWarning($"No permissions found for Role ID {request.RoleId}");
            return ApiResponseHelper.CreateErrorResponse<GetRolePermissionByRoleIdResponse>("No permissions found", 404);
        }

        // Mapear la lista de UserPermission a PermissionDto
        var permissionDtos = permissions.Adapt<List<PermissionDto>>();

        var response = new GetRolePermissionByRoleIdResponse
        {
            RoleId = request.RoleId,
            Permissions = permissionDtos
        };

        return ApiResponseHelper.CreateSuccessResponse(response, "Permissions fetched successfully");
    }
}
