using BicycleRental.Data.Enitities.Interfaces;

namespace BicycleRental.Data.Enitities
{
    public class Transaction : ITrackable
    {
        public Guid  Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BikeId { get; set; }
        public DateTime RentalStartTime { get; set; }
        public DateTime RentalEndTime { get; set; }
        public double RentalCost { get; set; }
        public bool IsDeleted { get; set; }
        public bool Paid { get; set; }
       
        public Bike Bike { get; set; }
        public User User { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
