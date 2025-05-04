using Microsoft.EntityFrameworkCore;
using My_vaccine_app.Literals;
using My_vaccine_app.Models;
using System.Runtime.CompilerServices;
namespace My_vaccine_app.Configurations
{
    public static class DbConfigurations
    {
        public static IServiceCollection SetDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyVaccineConnection")
                                    ?? "Server=DESKTOP-JLC71V1;Database=my_vaccine;Trusted_Connection=True;TrustServerCertificate=True;";

            services.AddDbContext<MyVaccineAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}