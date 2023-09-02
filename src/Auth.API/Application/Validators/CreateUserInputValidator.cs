
using Auth.API.Application.Inputs;
using FluentValidation;

namespace Auth.API.Application.Validators
{
    public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
    {
        public CreateUserInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(5).MaximumLength(100);
        }
    }
}