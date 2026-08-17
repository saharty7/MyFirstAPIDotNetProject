using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MyApi.Services;
using MyApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;



[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;
    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Car>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        if (cars == null || !cars.Any())
        {
            return NotFound("No cars found.");
        }
        return Ok(cars);
    }

    [HttpGet("make/{make}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Car>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Car>>> GetAllCarsByMake(string make)
    {
        var cars = await _carService.GetAllCarsByMakeAsync(make);
        if (cars == null || !cars.Any())
        {
            return NotFound($"No cars found for make: {make}");
        }
        return Ok(cars);
    }

    [HttpGet("id/{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Car), StatusCodes.Status200OK)]
    public async Task<ActionResult<Car>> GetCarById(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null)
        {
            return NotFound($"Car not found with ID: {id}");
        }
        return Ok(car);
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(Car), StatusCodes.Status201Created)]
    public async Task<ActionResult> AddCar([FromBody] Car car)
    {
        if (car == null)
        {
            return BadRequest("Car data is null!");
        }
        var result = await _carService.AddCarAsync(car);
        if (!result)
        {
            return StatusCode(500, "An error occurred while adding the car.");
        }
        return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
    }

    [HttpPut("id/{id}")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateCar(int id, [FromBody] Car car)
    {
        if (car == null)
        {
            return BadRequest("Car data is null!");
        }
        var result = await _carService.UpdateCarAsync(id, car);
        if (!result)
        {
            return NotFound($"Car not found with ID: {id}");
        }
        return NoContent();
    }

    [HttpDelete("id/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteCar(int id)
    {
        var result = await _carService.DeleteCarAsync(id);
        if (!result)
        {
            return NotFound($"Car not found with ID: {id}");
        }
        return NoContent();
    }
}
