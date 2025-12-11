using F1DriverApp.Api.DTOs;
using F1DriverApp.Api.Models;
using F1DriverApp.Api.Repositories;

namespace F1DriverApp.Api.Services;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _repository;
    
    public DriverService(IDriverRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<DriverDto>> GetAllDriversAsync()
    {
        var drivers = await _repository.GetAllAsync();
        return drivers.Select(MapToDto);
    }
    
    public async Task<DriverDto?> GetDriverByIdAsync(int id)
    {
        var driver = await _repository.GetByIdAsync(id);
        return driver == null ? null : MapToDto(driver);
    }
    
    public async Task<DriverDto> CreateDriverAsync(DriverDto driverDto)
    {
        var driver = MapToEntity(driverDto);
        var createdDriver = await _repository.CreateAsync(driver);
        return MapToDto(createdDriver);
    }
    
    public async Task<DriverDto?> UpdateDriverAsync(int id, DriverDto driverDto)
    {
        var driver = MapToEntity(driverDto);
        var updatedDriver = await _repository.UpdateAsync(id, driver);
        return updatedDriver == null ? null : MapToDto(updatedDriver);
    }
    
    public async Task<bool> DeleteDriverAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
    
    private static DriverDto MapToDto(Driver driver)
    {
        return new DriverDto
        {
            DriverId = driver.DriverId,
            Name = driver.Name,
            Team = driver.Team,
            Country = driver.Country,
            ChampionshipPoints = driver.ChampionshipPoints
        };
    }
    
    private static Driver MapToEntity(DriverDto dto)
    {
        return new Driver
        {
            DriverId = dto.DriverId,
            Name = dto.Name,
            Team = dto.Team,
            Country = dto.Country,
            ChampionshipPoints = dto.ChampionshipPoints
        };
    }
}
