using BicycleRental.Data;
using Microsoft.EntityFrameworkCore;

namespace BicycleRental.Extentions
{
    public static class DataBaseMigrateExtention
    {
        public static void MigrateDatabase(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            applicationDbContext.Database.Migrate();
        }
    }
}
