using ssptb.pe.tdlt.user.common.Responses;

namespace ssptb.pe.tdlt.user.data.Validations.Role;
public interface IRoleValidationService
{
    Task<ApiResponse<Guid>> ValidateRoleAsync(string roleName, CancellationToken cancellationToken);
}
