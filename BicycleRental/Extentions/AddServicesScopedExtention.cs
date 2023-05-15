using BicycleRental.Bussines.Services.Interfaces;
using BicycleRental.Bussines.Services;

namespace BicycleRental.Extentions
{
    public static class AddServicesScopedExtention
    {
        public static void AddServicesScoped(this IServiceCollection services)
        {      
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IBookingService,BookingService>();
            services.AddScoped<ILogService,LogService>();          
        }
    }
}
