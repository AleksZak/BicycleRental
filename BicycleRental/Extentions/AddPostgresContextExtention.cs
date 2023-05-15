using BicycleRental.Data;
using Microsoft.EntityFrameworkCore;

namespace BicycleRental.Extentions
{
    public static class AddPostgresContextExtention
    {
        public static void AddPostgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            services.AddDbContext<ApplicationContext>(builder => builder.UseNpgsql(connectionString));
        }
    }
}
