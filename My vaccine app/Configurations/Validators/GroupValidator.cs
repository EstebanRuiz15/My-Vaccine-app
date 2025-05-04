using FluentValidation;
using My_vaccine_app.Dtos.FamilyGroup;

namespace My_vaccine_app.Configurations.Validators
{
    public class GroupValidator: AbstractValidator<GroupRequestDto>
    {
        public GroupValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MaximumLength(225);
        }
    }
}
