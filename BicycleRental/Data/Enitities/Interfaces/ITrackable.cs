namespace BicycleRental.Data.Enitities.Interfaces
{
    public interface ITrackable
    {
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
