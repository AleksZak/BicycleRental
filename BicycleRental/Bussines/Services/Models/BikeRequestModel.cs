using BicycleRental.Data.Enitities.Enums;

namespace BicycleRental.Bussines.Services.Models
{
    public class BikeRequestModel
    {
        public string Name { get; set; } = null!;
        public BikeState State { get; set; }
        public double CostPerHour { get; set; }
    }
}
