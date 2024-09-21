using Microsoft.EntityFrameworkCore;

namespace ssptb.pe.tdlt.user.data.Validations.Role.Service;
public class RoleService : IRoleService
{
    private readonly ApplicationDbContext _context;

    public RoleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid?> GetRoleIdByNameAsync(string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        return role?.RoleId;
    }
}
