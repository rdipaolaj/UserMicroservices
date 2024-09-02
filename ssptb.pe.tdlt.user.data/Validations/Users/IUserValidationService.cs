using ssptb.pe.tdlt.user.command.Command;

namespace ssptb.pe.tdlt.user.data.Validations.Users;
public interface IUserValidationService
{
    Task ValidateUserAsync(CreateUserCommand request, CancellationToken cancellationToken);
}
