//using AutoMapper;
//using My_vaccine_app.Dtos.Vacccine;
//using My_vaccine_app.Models;
//using My_vaccine_app.Services.Contracts;

//namespace My_vaccine_app.Configurations.Resolvers
//{
//    public class CategoriesResolver : IValueResolver<Vaccine, VaccineResponseDto, string[]>
//    {
//        private readonly ICategoryService _categoryService;

//        public CategoriesResolver(ICategoryService categoryService)
//        {
//            _categoryService = categoryService;
//        }

//        public string[] Resolve(Vaccine source, VaccineResponseDto destination, string[] destMember, ResolutionContext context)
//        {
//            return source.Categories.Select(c => c.Name).ToArray();
//        }
//    }
//}
