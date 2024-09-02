using Microsoft.EntityFrameworkCore;
using ssptb.pe.tdlt.user.command.Command;

namespace ssptb.pe.tdlt.user.data.Validations.Users;
public class UserValidationService : IUserValidationService
{
    private readonly ApplicationDbContext _context;

    public UserValidationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ValidateUserAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
        {
            throw new Exception("Username already exists in the database.");
        }

        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            throw new Exception("Email already exists in the database.");
        }

        if (await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken))
        {
            throw new Exception("Phone number already exists in the database.");
        }
    }
}