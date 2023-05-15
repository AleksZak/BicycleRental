using BicycleRental.Data.Enitities;
using BicycleRental.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace BicycleRental.Data.Repositories
{
    public class BikeRepository : BaseRepository<Bike>, IBikeRepository
    {
        private readonly ApplicationContext _context;

        public BikeRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Bike>> GetBikesByLimitOffset(int offset, int limit)
        {
            var query = _context.Bikes.Where(x => x.State == Enitities.Enums.BikeState.Available).AsQueryable();
            return await query
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
        }
    }
}
