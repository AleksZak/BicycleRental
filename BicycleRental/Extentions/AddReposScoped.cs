using BicycleRental.Data.Repositories.Interfaces;
using BicycleRental.Data.Repositories;

namespace BicycleRental.Extentions
{
    public static class AddReposScoped
    {
        public static void AddRepositoriesScoped(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBikeRepository, BikeRepository>();
        }
    }
}
