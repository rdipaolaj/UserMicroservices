using Mapster;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.entities.Entities;

namespace ssptb.pe.tdlt.user.data.Validations.Users.Service;
public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(CreateUserCommand request, string salt, string hashedPassword, CancellationToken cancellationToken)
    {
        var user = request.Adapt<User>();
        user.HashedPassword = hashedPassword;
        user.SaltPassword = salt;

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }
}