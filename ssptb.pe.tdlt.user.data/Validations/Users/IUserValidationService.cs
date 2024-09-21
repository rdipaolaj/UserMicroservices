using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.common.Responses;

namespace ssptb.pe.tdlt.user.data.Validations.Users;
public interface IUserValidationService
{
    Task<ApiResponse<string>> ValidateUserAsync(CreateUserCommand request, CancellationToken cancellationToken);
}
