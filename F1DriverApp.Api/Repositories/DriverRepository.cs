using F1DriverApp.Api.Data;
using F1DriverApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace F1DriverApp.Api.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly F1DriverContext _context;
    
    public DriverRepository(F1DriverContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Driver>> GetAllAsync()
    {
        return await _context.Drivers.ToListAsync();
    }
    
    public async Task<Driver?> GetByIdAsync(int id)
    {
        return await _context.Drivers.FindAsync(id);
    }
    
    public async Task<Driver> CreateAsync(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
        return driver;
    }
    
    public async Task<Driver?> UpdateAsync(int id, Driver driver)
    {
        var existingDriver = await _context.Drivers.FindAsync(id);
        if (existingDriver == null)
        {
            return null;
        }
        
        existingDriver.Name = driver.Name;
        existingDriver.Team = driver.Team;
        existingDriver.Country = driver.Country;
        existingDriver.ChampionshipPoints = driver.ChampionshipPoints;
        
        await _context.SaveChangesAsync();
        return existingDriver;
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (driver == null)
        {
            return false;
        }
        
        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();
        return true;
    }
}
