using Microsoft.EntityFrameworkCore;
using ssptb.pe.tdlt.user.command.Command;
using ssptb.pe.tdlt.user.common.Responses;

namespace ssptb.pe.tdlt.user.data.Validations.Users;
public class UserValidationService : IUserValidationService
{
    private readonly ApplicationDbContext _context;

    public UserValidationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<string>> ValidateUserAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var validationErrors = new List<ErrorDetail>();

        if (await _context.Users.AnyAsync(u => u.Username == request.Username, cancellationToken))
        {
            validationErrors.Add(new ErrorDetail { Code = "USER0009", Description = "Username already exists in the database." });
        }

        if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            validationErrors.Add(new ErrorDetail { Code = "USER0010", Description = "Email already exists in the database." });
        }

        if (await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber, cancellationToken))
        {
            validationErrors.Add(new ErrorDetail { Code = "USER0011", Description = "Phone number already exists in the database." });
        }

        if (validationErrors.Any())
        {
            return ApiResponseHelper.CreateValidationErrorResponse(validationErrors);
        }

        return ApiResponseHelper.CreateSuccessResponse("Validation passed");
    }
}