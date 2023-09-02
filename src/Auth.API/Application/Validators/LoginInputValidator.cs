
using Auth.API.Application.Inputs;
using FluentValidation;

namespace Auth.API.Application.Validators
{
    public class LoginInputValidator : AbstractValidator<LoginInput>
    {
        public LoginInputValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(100);
        }
    }
}