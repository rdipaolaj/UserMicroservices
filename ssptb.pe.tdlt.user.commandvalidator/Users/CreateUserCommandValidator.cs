using FluentValidation;
using ssptb.pe.tdlt.user.command.Command;

namespace ssptb.pe.tdlt.user.commandvalidator.Users;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .WithErrorCode("USER0001"); // Código personalizado para el Username

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .WithErrorCode("USER0002"); // Código personalizado para el Email

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .WithErrorCode("USER0003"); // Código personalizado para el PhoneNumber

        RuleFor(x => x.HashedPassword)
            .NotEmpty()
            .WithMessage("HashedPassword is required")
            .WithErrorCode("USER0004"); // Código personalizado para el HashedPassword

        RuleFor(x => x.UserRole)
            .NotEmpty()
            .WithMessage("UserRole is required")
            .WithErrorCode("USER0005"); // Código personalizado para el UserRole

        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("CompanyName is required")
            .WithErrorCode("USER0006"); // Código personalizado para el CompanyName

        RuleFor(x => x.Department)
            .NotEmpty()
            .WithMessage("Department is required")
            .WithErrorCode("USER0007"); // Código personalizado para el Department

        RuleFor(x => x.JobTitle)
            .NotEmpty()
            .WithMessage("JobTitle is required")
            .WithErrorCode("USER0008"); // Código personalizado para el JobTitle
    }
}
