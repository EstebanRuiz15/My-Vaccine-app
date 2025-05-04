using AutoMapper;
using My_vaccine_app.Dtos.Vacccine;
using My_vaccine_app.Models;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Configurations.Resolvers
{
    public class VaccineCategoryResolver : IValueResolver<VaccineRequestDto, Vaccine, List<VaccineCategory>>
    {
        private readonly ICategoryService _categoryService;

        public VaccineCategoryResolver(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<VaccineCategory> Resolve(VaccineRequestDto source, Vaccine destination, List<VaccineCategory> destMember, ResolutionContext context)
        {
            if (source.Categories == null || !source.Categories.Any())
                return new List<VaccineCategory>();

            var allCategories = _categoryService.GetAllCategories().Result;

            return allCategories
                .Where(c => source.Categories.Any(sc =>
                    string.Equals(sc, c.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(c => new VaccineCategory
                {
                    Name = c.Name
                })
                .ToList();
        }
    }
}
