using FluentValidation;
using ssptb.pe.tdlt.user.command.Command;

namespace ssptb.pe.tdlt.user.commandvalidator.Users;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        RuleFor(x => x.HashedPassword).NotEmpty().WithMessage("HashedPassword is required");
        RuleFor(x => x.UserRole).NotEmpty().WithMessage("UserRole is required");
        RuleFor(x => x.CompanyName).NotEmpty().WithMessage("CompanyName is required");
        RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
        RuleFor(x => x.JobTitle).NotEmpty().WithMessage("JobTitle is required");
    }
}
