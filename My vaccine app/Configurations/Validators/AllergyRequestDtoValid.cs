using FluentValidation;
using My_vaccine_app.Dtos.Allergies;

namespace My_vaccine_app.Configurations.Validators
{
    public class AllergyRequestDtoValid : AbstractValidator<AllergyRequestDto>
    {
        public AllergyRequestDtoValid()
        {
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(225);
            RuleFor(dto => dto.UserId).NotEmpty().GreaterThan(0);
        }
    }
}
