using Microsoft.EntityFrameworkCore;
using MyApi.Models;
using MyApi.Data;

namespace MyApi.Services;

public class CarService : ICarService
{
    private readonly ApplicationDbContext _context;
    public CarService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        if (_context.Cars == null)
        {
            throw new InvalidOperationException("Cars DbSet is not initialized.");
        }
        return await _context.Cars.ToListAsync();
    }
    public async Task<IEnumerable<Car>> GetAllCarsByMakeAsync(string make)
    {
        var cars = await _context.Cars.Where(c => c.Make == make).ToListAsync();
        if (cars == null || !cars.Any())
        {
            throw new KeyNotFoundException($"No cars found for make: {make}");
        }
        return cars;
    }
    public async Task<Car> GetCarByIdAsync(int id)
    {
        var currCar = await _context.Cars.FindAsync(id);
        if (currCar == null)
        {
            throw new KeyNotFoundException($"Car with ID {id} not found.");
        }
        return currCar;
    }
    public async Task<bool> AddCarAsync(Car car)
    {
        if (string.IsNullOrWhiteSpace(car.Make) || string.IsNullOrWhiteSpace(car.Model) || car.Year <= 0)
        {
            throw new ArgumentException("Invalid car data.");
        }
        await _context.Cars.AddAsync(car);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateCarAsync(int id, Car car)
    {
        var currCar = await _context.Cars.FindAsync(id);
        if (currCar == null)
        {
            throw new KeyNotFoundException($"Car with ID {id} not found.");
        }
        currCar.Make = car.Make;
        currCar.Model = car.Model;
        currCar.Year = car.Year;
        currCar.Price = car.Price;
        currCar.IsAvailable = car.IsAvailable;
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        var currCar = await _context.Cars.FindAsync(id);
        if (currCar == null)
        {
            throw new KeyNotFoundException($"Car with ID {id} not found.");
        }
        _context.Cars.Remove(currCar);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

}