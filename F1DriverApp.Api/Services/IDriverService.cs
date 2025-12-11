using F1DriverApp.Api.DTOs;

namespace F1DriverApp.Api.Services;

public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetAllDriversAsync();
    Task<DriverDto?> GetDriverByIdAsync(int id);
    Task<DriverDto> CreateDriverAsync(DriverDto driverDto);
    Task<DriverDto?> UpdateDriverAsync(int id, DriverDto driverDto);
    Task<bool> DeleteDriverAsync(int id);
}
