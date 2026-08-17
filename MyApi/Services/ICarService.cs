using MyApi.Models;

namespace MyApi.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<IEnumerable<Car>> GetAllCarsByMakeAsync(string make);
    Task<Car> GetCarByIdAsync(int id);
    Task<bool> AddCarAsync(Car car);
    Task<bool> UpdateCarAsync(int id, Car car);
    Task<bool> DeleteCarAsync(int id);
}