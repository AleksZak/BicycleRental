namespace BicycleRental.Bussines.Services.Interfaces
{
    public interface IBookingService
    {
        public Task<string> BookBikeById(Guid bikeId, DateTime start, DateTime end);
        public Task<string> RentBikeByTransactionId(Guid transactionId);
        public Task<string> PayForRentByTransactionId(Guid transactionId);
        public Task<string> ReturnBike(Guid transactionId);
    }
}
