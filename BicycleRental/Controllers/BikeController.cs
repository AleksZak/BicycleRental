using BicycleRental.Bussines.Services.Models;
using BicycleRental.Data.Enitities;
using BicycleRental.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BicycleRental.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeRepository _bikeRepository;
        public BikeController(IBikeRepository bikeRepository)
        {
            _bikeRepository = bikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get( int offset,int limit)
        {
            return Ok(await _bikeRepository.GetBikesByLimitOffset(offset,limit));
        }

        // GET api/<BikeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _bikeRepository.GetByIdAsync(id));
        }

        // POST api/<BikeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BikeRequestModel model)
        {
            await _bikeRepository.AddAsync(new Bike {Name = model.Name,State = model.State, CostPerHour = model.CostPerHour });
            return Ok();
        }

        // PUT api/<BikeController>/5
        [HttpPut("{id}")]
        public async Task Put(Bike bike)
        {
            await _bikeRepository.UpdateAsync(bike);
        }

        // DELETE api/<BikeController>/5
        [HttpDelete("{id}")]
        public async  Task Delete(Guid id)
        {
            await _bikeRepository.DeleteAsync(new Bike { Id = id,State = Data.Enitities.Enums.BikeState.UnAvailable});
        }
    }
}
