//using AutoMapper;
//using My_vaccine_app.Dtos.Vacccine;
//using My_vaccine_app.Models;
//using My_vaccine_app.Services.Contracts;

//namespace My_vaccine_app.Configurations.Resolvers
//{
//    public class CategoriesReverseResolver : IValueResolver<VaccineResponseDto, Vaccine, List<VaccineCategory>>
//    {
//        private readonly ICategoryService _categoryService;

//        public CategoriesReverseResolver(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }

//        public List<VaccineCategory> Resolve(VaccineResponseDto source, Vaccine destination, List<VaccineCategory> destMember, ResolutionContext context)
//        {
//            var allCategories = _categoryService.GetAllCategories().Result;
//            return allCategories
//                .Where(c => source.Categories.Any(sc =>
//                    string.Equals(sc, c.Name, StringComparison.OrdinalIgnoreCase)))
//                .ToList();
//        }
//    }
//}
