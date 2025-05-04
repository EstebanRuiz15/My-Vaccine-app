using FluentValidation;
using My_vaccine_app.Dtos.Categories;

namespace My_vaccine_app.Configurations.Validators
{
    public class CategoiresValidator : AbstractValidator <CategoriRequestDto>
    {
        public CategoiresValidator() 
        { 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot by null");
        
        }
    }
}
