namespace ssptb.pe.tdlt.user.data.Validations.Role.Service;
public interface IRoleService
{
    Task<Guid?> GetRoleIdByNameAsync(string roleName);
}
