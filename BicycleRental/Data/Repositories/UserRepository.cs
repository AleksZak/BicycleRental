using BicycleRental.Data.Enitities;
using BicycleRental.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BicycleRental.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<string>> GetAllUsersEmailsAsync()
        {
            return await _context.Users.Select(x => x.Email).ToListAsync();
        }
        
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);

        }

        public async Task<bool> IsExistAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
  }
}