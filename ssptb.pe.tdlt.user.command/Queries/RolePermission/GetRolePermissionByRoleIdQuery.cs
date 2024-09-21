using MediatR;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.dto.Dtos.RolePermission;

namespace ssptb.pe.tdlt.user.command.Queries.RolePermission;
public class GetRolePermissionByRoleIdQuery : IRequest<ApiResponse<GetRolePermissionByRoleIdResponse>>
{
    public Guid RoleId { get; }

    public GetRolePermissionByRoleIdQuery(Guid roleId)
    {
        RoleId = roleId;
    }
}
