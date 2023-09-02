using Employee.API.Application.Inputs;
using FluentValidation;

namespace Employee.API.Application.Validators
{
    public class UpdateEmployeeInputValidator : AbstractValidator<UpdateEmployeeInput>
    {
        public UpdateEmployeeInputValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(x => x.Document).NotEmpty();
            RuleFor(x => x.BirthedAt).GreaterThan(DateTime.MinValue).LessThan(DateTime.UtcNow);
        }
    }
}