using F1DriverApp.Api.DTOs;
using F1DriverApp.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace F1DriverApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;
    
    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }
    
    /// <summary>
    /// Get all drivers
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DriverDto>>> GetAllDrivers()
    {
        var drivers = await _driverService.GetAllDriversAsync();
        return Ok(drivers);
    }
    
    /// <summary>
    /// Get a specific driver by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<DriverDto>> GetDriver(int id)
    {
        var driver = await _driverService.GetDriverByIdAsync(id);
        
        if (driver == null)
        {
            return NotFound(new { message = $"Driver with ID {id} not found" });
        }
        
        return Ok(driver);
    }
    
    /// <summary>
    /// Create a new driver
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<DriverDto>> CreateDriver([FromBody] DriverDto driverDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var createdDriver = await _driverService.CreateDriverAsync(driverDto);
        return CreatedAtAction(nameof(GetDriver), new { id = createdDriver.DriverId }, createdDriver);
    }
    
    /// <summary>
    /// Update an existing driver
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<DriverDto>> UpdateDriver(int id, [FromBody] DriverDto driverDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var updatedDriver = await _driverService.UpdateDriverAsync(id, driverDto);
        
        if (updatedDriver == null)
        {
            return NotFound(new { message = $"Driver with ID {id} not found" });
        }
        
        return Ok(updatedDriver);
    }
    
    /// <summary>
    /// Delete a driver
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDriver(int id)
    {
        var result = await _driverService.DeleteDriverAsync(id);
        
        if (!result)
        {
            return NotFound(new { message = $"Driver with ID {id} not found" });
        }
        
        return NoContent();
    }
}
