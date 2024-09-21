using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.data.Validations.RolePermission.Service;
public interface IRolePermissionService
{
    Task<List<UserPermission>> GetPermissionsByRoleIdAsync(Guid roleId);
}
