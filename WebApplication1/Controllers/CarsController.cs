using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;
//using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarContext _db;
        private readonly CarRepository _repository;
      

        public CarsController(CarContext context)
        {
            _repository = new CarRepository(context);
            _db = context;
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> Get(int id)
        {
            var car = await _repository.GetByIdAsync(id);
            if (car == null)
                return NotFound();
            return car;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> Post(Car car)
        {
            if (car == null)
                return BadRequest();

            await _repository.AddAsync(car);

            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> Put(int id, [FromBody] Car updatedCar)
        {
            if (updatedCar == null)
                return BadRequest("Данные автомобиля не могут быть пустыми.");
            var car = await _db.Car.FirstOrDefaultAsync(x => x.car_id == id);

            car.power = updatedCar.power;
            car.price = updatedCar.price;
            car.firm = updatedCar.firm;
            car.model = updatedCar.model;
            car.color = updatedCar.color;
            car.year = updatedCar.year;
            await _db.SaveChangesAsync();


            return Ok(car);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> Delete(int id)
        {
            var car = await _repository.GetByIdAsync(id);
            if (car == null)
                return NotFound();

            await _repository.DeleteAsync(id);

            return Ok(car);
        }
    }
}
