namespace BicycleRental.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public Task<TEntity?> GetByIdAsync(Guid id);
        public Task AddAsync(TEntity entity);
        public Task AddRangeAsync(List<TEntity> entities);
        public Task UpdateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
    }
}
