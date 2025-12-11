using F1DriverApp.Api.Models;

namespace F1DriverApp.Api.Repositories;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync();
    Task<Driver?> GetByIdAsync(int id);
    Task<Driver> CreateAsync(Driver driver);
    Task<Driver?> UpdateAsync(int id, Driver driver);
    Task<bool> DeleteAsync(int id);
}
