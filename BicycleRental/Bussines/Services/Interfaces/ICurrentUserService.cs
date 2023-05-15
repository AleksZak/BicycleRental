namespace BicycleRental.Bussines.Services.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
