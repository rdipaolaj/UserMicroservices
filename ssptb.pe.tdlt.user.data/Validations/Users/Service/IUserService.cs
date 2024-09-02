using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.data.Validations.Users.Service;
public interface IUserService
{
    Task<User> CreateUserAsync(CreateUserCommand request, string salt, string hashedPassword, CancellationToken cancellationToken);
}
