using BicycleRental.Data.Enitities.Interfaces;

namespace BicycleRental.Data.Enitities
{
    public class User : ITrackable
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;      
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
