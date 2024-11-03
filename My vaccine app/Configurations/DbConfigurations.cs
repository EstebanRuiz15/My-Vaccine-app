using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Literals;
using My_vaccine_app.Models;
using System.Runtime.CompilerServices;

namespace My_vaccine_app.Configurations
{
    public static class DbConfigurations
    {
        public static IServiceCollection SetDatabaseConfig(this IServiceCollection Services)
        {
            Services.AddDbContext<MyVaccineAppDbContext>(options =>
                  options.UseSqlServer("Server=localhost,1433;Database=MyVaccineDB;User Id=sa;Password=YourStrong@Passw0rd;Trusted_Connection=False;TrustServerCertificate=True;MultipleActiveResultSets=true"));
            return Services;
        }
    }//Environment.GetEnvironmentVariable(MyVaccineLiterals.MY_VACCINE_CONECTION_APP))
}
