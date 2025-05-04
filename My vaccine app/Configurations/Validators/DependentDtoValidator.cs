using FluentValidation;
using My_vaccine_app.Dtos.Dependent;

namespace My_vaccine_app.Configurations.Validators
{
    public class DependentDtoValidator : AbstractValidator<DependentRequestDto>
    {
        public DependentDtoValidator()
        { 
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(225);
            RuleFor(dto => dto.DateOfBirth).NotEmpty();
            RuleFor(dto => dto.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
