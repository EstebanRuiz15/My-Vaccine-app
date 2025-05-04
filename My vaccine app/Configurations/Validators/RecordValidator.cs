using FluentValidation;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Dtos.VaccineRecord;

namespace My_vaccine_app.Configurations.Validators
{
    public class RecordValidator : AbstractValidator<RecordRequestDto>
    {
        public RecordValidator()
        {
            RuleFor(n => n.VaccineId).NotEmpty().WithMessage("The vaccine cannot be empty");
            RuleFor(n => n.AdministeredLocation).NotNull().WithMessage("The location administered cannot by empty");
            RuleFor(n => n.AdministeredBy).NotEmpty().WithMessage("Administered by cannot by null");
            RuleFor(n => n.UserId).NotNull().WithMessage("User id cannot be null");
        }
    }
    
}
