using My_vaccine_app.Models;
using My_vaccine_app.Repositories.Implementations;
using My_vaccine_app.Repositories.Interfaces;
using My_vaccine_app.Services.Contracts;
using My_vaccine_app.Services.Implements;

namespace My_vaccine_app.Configurations.Inyections
{
    public static class CategoriesInyection
    {
        public static IServiceCollection SetCategoriesInyections(this IServiceCollection services)
        {

            #region repositories injection

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBaseRepository<VaccineCategory>, BaseRepository<VaccineCategory>>();
            #endregion

            #region services injection
            services.AddScoped<ICategoryService, CategoryService>();
            #endregion

            return services;
        }
    }
}
