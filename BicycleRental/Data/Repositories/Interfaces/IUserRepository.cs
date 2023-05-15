using BicycleRental.Data.Enitities;

namespace BicycleRental.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<List<string>> GetAllUsersEmailsAsync();
        public Task<User> GetUserByEmailAsync(string email);
        public Task<bool> IsExistAsync(string email);     
        public Task<User> GetUserByPhoneNumber(string phoneNumber);
    }
}
