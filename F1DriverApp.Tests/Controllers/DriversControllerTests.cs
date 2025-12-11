using F1DriverApp.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace F1DriverApp.Tests.Controllers;

public class DriversControllerTests
{
    private readonly Mock<IDriverService> _mockService;
    private readonly DriversController _controller;

    public DriversControllerTests()
    {
        _mockService = new Mock<IDriverService>();
        _controller = new DriversController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllDrivers_ReturnsOkResultWithDrivers()
    {
        // Arrange
        var drivers = new List<DriverDto>
        {
            new DriverDto
            {
                DriverId = 1,
                Name = "Max Verstappen",
                Team = "Red Bull Racing",
                Country = "Netherlands",
                ChampionshipPoints = 575
            }
        };
        _mockService.Setup(service => service.GetAllDriversAsync()).ReturnsAsync(drivers);

        // Act
        var result = await _controller.GetAllDrivers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDrivers = Assert.IsAssignableFrom<IEnumerable<DriverDto>>(okResult.Value);
        Assert.Single(returnedDrivers);
    }

    [Fact]
    public async Task GetDriver_WithValidId_ReturnsOkResultWithDriver()
    {
        // Arrange
        var driver = new DriverDto
        {
            DriverId = 1,
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 575
        };
        _mockService.Setup(service => service.GetDriverByIdAsync(1)).ReturnsAsync(driver);

        // Act
        var result = await _controller.GetDriver(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDriver = Assert.IsType<DriverDto>(okResult.Value);
        Assert.Equal("Max Verstappen", returnedDriver.Name);
    }

    [Fact]
    public async Task GetDriver_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetDriverByIdAsync(999)).ReturnsAsync((DriverDto?)null);

        // Act
        var result = await _controller.GetDriver(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDriver_WithValidDto_ReturnsCreatedAtAction()
    {
        // Arrange
        var driverDto = new DriverDto
        {
            Name = "Charles Leclerc",
            Team = "Ferrari",
            Country = "Monaco",
            ChampionshipPoints = 308
        };

        var createdDriver = new DriverDto
        {
            DriverId = 3,
            Name = "Charles Leclerc",
            Team = "Ferrari",
            Country = "Monaco",
            ChampionshipPoints = 308
        };

        _mockService.Setup(service => service.CreateDriverAsync(driverDto))
            .ReturnsAsync(createdDriver);

        // Act
        var result = await _controller.CreateDriver(driverDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDriver = Assert.IsType<DriverDto>(createdResult.Value);
        Assert.Equal(3, returnedDriver.DriverId);
        Assert.Equal("Charles Leclerc", returnedDriver.Name);
    }

    [Fact]
    public async Task UpdateDriver_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var driverDto = new DriverDto
        {
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 600
        };

        _mockService.Setup(service => service.UpdateDriverAsync(1, driverDto))
            .ReturnsAsync(driverDto);

        // Act
        var result = await _controller.UpdateDriver(1, driverDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDriver = Assert.IsType<DriverDto>(okResult.Value);
        Assert.Equal(600, returnedDriver.ChampionshipPoints);
    }

    [Fact]
    public async Task UpdateDriver_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var driverDto = new DriverDto
        {
            Name = "Test Driver",
            Team = "Test Team",
            Country = "Test Country",
            ChampionshipPoints = 0
        };

        _mockService.Setup(service => service.UpdateDriverAsync(999, driverDto))
            .ReturnsAsync((DriverDto?)null);

        // Act
        var result = await _controller.UpdateDriver(999, driverDto);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteDriver_WithValidId_ReturnsNoContent()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteDriverAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _controller.DeleteDriver(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteDriver_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteDriverAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _controller.DeleteDriver(999);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
