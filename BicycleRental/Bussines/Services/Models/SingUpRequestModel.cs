﻿namespace BicycleRental.Bussines.Services.Models
{
    public class SingUpRequestModel
    {
        public string FullName { get; set; } = null!;     
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
