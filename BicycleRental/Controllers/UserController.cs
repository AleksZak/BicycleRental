using BicycleRental.Bussines.Services.Interfaces;
using BicycleRental.Bussines.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BicycleRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IBookingService _bookingService;
      
        public UserController(ILogService logService, IBookingService bookingService)
        {
            _logService = logService;
            _bookingService = bookingService;

        }

        [HttpGet("signin")]
        public async Task<string> SignIn(string email, string password)
        {
            return await _logService.SignInAsync(email, password);
        }

        [HttpPost("signup")]
        public async Task SignUp(SingUpRequestModel model)
        {
            await _logService.SingnUpAsync(model);
        }

        [HttpPost("{bikeId}/book")]
        [Authorize]
        public async Task<IActionResult> BookBike(Guid bikeId,DateTime start, DateTime end)
        {
           return Ok(await _bookingService.BookBikeById(bikeId, start, end)) ;
        }

        [HttpPut("{transactionId}/pay")]
        [Authorize]
        public async Task<IActionResult> PayFoRent(Guid transactionId )
        {
            return Ok( await _bookingService.PayForRentByTransactionId(transactionId));
        }

        [HttpPut("{transactionId}/rent")]
        [Authorize]
        public async Task<IActionResult> RentBike(Guid transactionId)
        {
            return Ok(await _bookingService.RentBikeByTransactionId(transactionId));
        }

        [HttpPut("{transactionId}/return")]
        [Authorize]
        public async Task<IActionResult> ReturnИike(Guid transactionId)
        {
            return Ok(await _bookingService.ReturnBike(transactionId));
        }

    }        
}