using BicycleRental.Bussines.Services.Interfaces;
using BicycleRental.Data.Enitities;
using BicycleRental.Data.Repositories.Interfaces;

namespace BicycleRental.Bussines.Services
{
    public class BookingService : IBookingService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBikeRepository _bikeRepository;

        public BookingService(ITransactionRepository transactionRepository, IBikeRepository bikeRepository, ICurrentUserService currentUserService)
        {
            _transactionRepository = transactionRepository;
            _bikeRepository = bikeRepository;
            _currentUserService = currentUserService;

        }

        public async Task<string> BookBikeById(Guid bikeId, DateTime start, DateTime end)
        {    
            var bike = await _bikeRepository.GetByIdAsync(bikeId) ?? throw new Exception("Bike not found");
            var diff = end - start;
            
            if(diff.TotalHours < 0)
            {
                throw new Exception("You select wrong date");
            }

            var rentCost = diff.TotalHours * bike.CostPerHour;
            var transaction = new Transaction { BikeId = bikeId, UserId = _currentUserService.UserId ,RentalStartTime = start, RentalEndTime = end,RentalCost = rentCost,Paid = false};

            bike.State = Data.Enitities.Enums.BikeState.Booked;

           await _transactionRepository.AddAsync(transaction);
           await _bikeRepository.UpdateAsync(bike);
            
            return $"Your bike {bike.Name} successfuly booked transaction id {transaction.Id} is will be cost {transaction.RentalCost} ";

    }

        public async Task<string> PayForRentByTransactionId(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId) ?? throw new Exception("Transaction not found");
            
            transaction.Paid = true;
            
            await _transactionRepository.UpdateAsync(transaction);

            return "Payment was successful";
        }

        public async Task<string> RentBikeByTransactionId(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId) ?? throw new Exception("Transaction not found");

            if(transaction.Paid == false)
            {
                return "Please pay for rent";
            }

            return $"Now you can get your bike {transaction.BikeId}";

        }

        public async Task<string> ReturnBike( Guid transactionId)
        {
            var transaction = await _transactionRepository.GetByIdAsync(transactionId) ?? throw new Exception("Transaction not found");
            var bike = await _bikeRepository.GetByIdAsync(transaction.BikeId);
            var now = DateTime.UtcNow;
            var diff = now - transaction.RentalEndTime;            
            if(diff.Hours > 0)
            {
                var refund = bike.CostPerHour * diff.Hours;
                return $"Your refund will be {refund}";
            }
            else if(diff.Hours == 0)
            {
                return "Thank You for renting our bike";
            }
            else if(diff.Hours < 0)
            {
                var additionalPayment = bike.CostPerHour * Math.Abs(diff.Hours) ;
                return $"You need to pay {additionalPayment}";
            }

            bike.State = Data.Enitities.Enums.BikeState.Available;
           await  _bikeRepository.UpdateAsync(bike);
            return "Thank You for renting our bike";
        }
    }
}
