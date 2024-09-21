using Microsoft.EntityFrameworkCore;
using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.data.Validations.RolePermission.Service;
public class RolePermissionService : IRolePermissionService
{
    private readonly ApplicationDbContext _context;

    public RolePermissionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserPermission>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Include(rp => rp.Permission)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}
