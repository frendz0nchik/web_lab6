using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class CarRepository
    {
        private readonly CarContext _context;

        public CarRepository(CarContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Car.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Car.FirstOrDefaultAsync(x => x.car_id == id);
        }

        public async Task AddAsync(Car car)
        {
            _context.Car.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            _context.Car.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var car = await _context.Car.FirstOrDefaultAsync(x => x.car_id == id);
            if (car != null)
            {
                _context.Car.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}