namespace F1DriverApp.Tests.Services;

public class DriverServiceTests
{
    private readonly Mock<IDriverRepository> _mockRepository;
    private readonly DriverService _service;

    public DriverServiceTests()
    {
        _mockRepository = new Mock<IDriverRepository>();
        _service = new DriverService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllDriversAsync_ReturnsAllDrivers()
    {
        // Arrange
        var drivers = new List<Driver>
        {
            new Driver
            {
                DriverId = 1,
                Name = "Max Verstappen",
                Team = "Red Bull Racing",
                Country = "Netherlands",
                ChampionshipPoints = 575
            },
            new Driver
            {
                DriverId = 2,
                Name = "Lewis Hamilton",
                Team = "Mercedes",
                Country = "United Kingdom",
                ChampionshipPoints = 234
            }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(drivers);

        // Act
        var result = await _service.GetAllDriversAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Max Verstappen", result.First().Name);
        _mockRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetDriverByIdAsync_WithValidId_ReturnsDriver()
    {
        // Arrange
        var driver = new Driver
        {
            DriverId = 1,
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 575
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(driver);

        // Act
        var result = await _service.GetDriverByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Max Verstappen", result.Name);
        Assert.Equal("Red Bull Racing", result.Team);
        Assert.Equal(575, result.ChampionshipPoints);
    }

    [Fact]
    public async Task GetDriverByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Driver?)null);

        // Act
        var result = await _service.GetDriverByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateDriverAsync_WithValidDto_ReturnsCreatedDriver()
    {
        // Arrange
        var driverDto = new DriverDto
        {
            Name = "Charles Leclerc",
            Team = "Ferrari",
            Country = "Monaco",
            ChampionshipPoints = 308
        };

        var createdDriver = new Driver
        {
            DriverId = 3,
            Name = "Charles Leclerc",
            Team = "Ferrari",
            Country = "Monaco",
            ChampionshipPoints = 308
        };

        _mockRepository.Setup(repo => repo.CreateAsync(It.IsAny<Driver>()))
            .ReturnsAsync(createdDriver);

        // Act
        var result = await _service.CreateDriverAsync(driverDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.DriverId);
        Assert.Equal("Charles Leclerc", result.Name);
        Assert.Equal("Ferrari", result.Team);
        _mockRepository.Verify(repo => repo.CreateAsync(It.IsAny<Driver>()), Times.Once);
    }

    [Fact]
    public async Task UpdateDriverAsync_WithValidId_ReturnsUpdatedDriver()
    {
        // Arrange
        var driverDto = new DriverDto
        {
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 600
        };

        var updatedDriver = new Driver
        {
            DriverId = 1,
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 600
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(1, It.IsAny<Driver>()))
            .ReturnsAsync(updatedDriver);

        // Act
        var result = await _service.UpdateDriverAsync(1, driverDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(600, result.ChampionshipPoints);
    }

    [Fact]
    public async Task UpdateDriverAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var driverDto = new DriverDto
        {
            Name = "Test Driver",
            Team = "Test Team",
            Country = "Test Country",
            ChampionshipPoints = 0
        };

        _mockRepository.Setup(repo => repo.UpdateAsync(999, It.IsAny<Driver>()))
            .ReturnsAsync((Driver?)null);

        // Act
        var result = await _service.UpdateDriverAsync(999, driverDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteDriverAsync_WithValidId_ReturnsTrue()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteDriverAsync(1);

        // Assert
        Assert.True(result);
        _mockRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteDriverAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync(999)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteDriverAsync(999);

        // Assert
        Assert.False(result);
    }
}
