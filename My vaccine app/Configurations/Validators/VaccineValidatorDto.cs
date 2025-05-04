using FluentValidation;
using My_vaccine_app.Dtos.Vacccine;

namespace My_vaccine_app.Configurations.Validators
{
    public class VaccineValidatorDto : AbstractValidator<VaccineRequestDto>
    {
       public VaccineValidatorDto() 
        { 
            RuleFor(n => n.Name).NotEmpty().MaximumLength(225);
            RuleFor(n => n.RequieredBooster).NotNull().WithMessage("RequieredBooster must be true or false.");
            RuleFor(n => n.Categories).NotEmpty().WithMessage("must have at least one associated category");
        }
    }
}
