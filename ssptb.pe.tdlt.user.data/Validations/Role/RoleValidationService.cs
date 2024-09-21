using Microsoft.EntityFrameworkCore;
using ssptb.pe.tdlt.user.common.Responses;
using ssptb.pe.tdlt.user.data.Validations.Role.Service;

namespace ssptb.pe.tdlt.user.data.Validations.Role;
public class RoleValidationService : IRoleValidationService
{
    private readonly IRoleService _roleService;

    public RoleValidationService(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<ApiResponse<Guid>> ValidateRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        var roleId = await _roleService.GetRoleIdByNameAsync(roleName);
        if (roleId == null)
        {
            var validationErrors = new List<ErrorDetail>
            {
                new ErrorDetail { Code = "USER0012", Description = "El rol especificado no es válido." }
            };
            return ApiResponseHelper.CreateValidationErrorResponse<Guid>(validationErrors);
        }

        return ApiResponseHelper.CreateSuccessResponse(roleId.Value, "Role validation passed");
    }
}
