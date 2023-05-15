using BicycleRental.Data.Enitities;

namespace BicycleRental.Data.Repositories.Interfaces
{
    public interface IBikeRepository : IBaseRepository<Bike>
    {
        public Task<List<Bike>> GetBikesByLimitOffset(int offset, int limit);
    }
}
