using AutoMapper;
using My_vaccine_app.Models;
using My_vaccine_app.Services.Contracts;

namespace My_vaccine_app.Configurations.Resolvers
{
    public class StringToCategoryListConverter : ITypeConverter<List<string>, List<VaccineCategory>>
    {
        private readonly ICategoryService _categoryService;

        public StringToCategoryListConverter(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public List<VaccineCategory> Convert(List<string> source, List<VaccineCategory> destination, ResolutionContext context)
        {
            var allCategories = _categoryService.GetAllCategories().Result;
            return allCategories
                .Where(c => source.Any(sc =>
                    string.Equals(sc, c.Name, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }
    }
}
