using Events.ItAcademy.Application.Users.Requests;
using FluentValidation;

namespace Events.ItAcademy.API.Infrastructure.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserCreateRequestModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("This field is required")
                .EmailAddress().WithMessage("Enter email in correct format");

            RuleFor(x => x.Password)
           .MinimumLength(8)
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[0-9]").WithMessage("Password must contain at least one number.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }
    public class UserLoginValidator : AbstractValidator<UserLoginRequestModel>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("This field is required")
               .EmailAddress().WithMessage("Enter email in correct format");

            RuleFor(x => x.Password)
           .MinimumLength(5)
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches("[0-9]").WithMessage("Password must contain at least one number.")
           .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }
}
