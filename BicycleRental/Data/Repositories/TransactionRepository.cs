using BicycleRental.Data.Enitities;
using BicycleRental.Data.Repositories.Interfaces;

namespace BicycleRental.Data.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
