using BicycleRental.Data.Enitities.Enums;
using BicycleRental.Data.Enitities.Interfaces;

namespace BicycleRental.Data.Enitities
{
    public class Bike : ITrackable
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public BikeState State { get; set; }
        public double CostPerHour { get; set; }

        public Transaction Transaction { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
