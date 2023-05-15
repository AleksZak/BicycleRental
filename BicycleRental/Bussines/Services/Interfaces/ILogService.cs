using BicycleRental.Bussines.Services.Models;

namespace BicycleRental.Bussines.Services.Interfaces
{
    public interface ILogService
    {
        public Task<string> SignInAsync(string email, string password);
        public Task SingnUpAsync(SingUpRequestModel model);
    }
}
 