using F1DriverApp.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace F1DriverApp.Tests.Repositories;

public class DriverRepositoryTests
{
    private F1DriverContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<F1DriverContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new F1DriverContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllDrivers()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        context.Drivers.AddRange(
            new Driver
            {
                Name = "Max Verstappen",
                Team = "Red Bull Racing",
                Country = "Netherlands",
                ChampionshipPoints = 575
            },
            new Driver
            {
                Name = "Lewis Hamilton",
                Team = "Mercedes",
                Country = "United Kingdom",
                ChampionshipPoints = 234
            }
        );
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsDriver()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        var driver = new Driver
        {
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 575
        };
        context.Drivers.Add(driver);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByIdAsync(driver.DriverId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Max Verstappen", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsDriverToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        var driver = new Driver
        {
            Name = "Charles Leclerc",
            Team = "Ferrari",
            Country = "Monaco",
            ChampionshipPoints = 308
        };

        // Act
        var result = await repository.CreateAsync(driver);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.DriverId > 0);
        Assert.Equal("Charles Leclerc", result.Name);
        Assert.Single(context.Drivers);
    }

    [Fact]
    public async Task UpdateAsync_WithValidId_UpdatesDriver()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        var driver = new Driver
        {
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 575
        };
        context.Drivers.Add(driver);
        await context.SaveChangesAsync();

        var updatedDriver = new Driver
        {
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 600
        };

        // Act
        var result = await repository.UpdateAsync(driver.DriverId, updatedDriver);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(600, result.ChampionshipPoints);
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        var driver = new Driver
        {
            Name = "Test Driver",
            Team = "Test Team",
            Country = "Test Country",
            ChampionshipPoints = 0
        };

        // Act
        var result = await repository.UpdateAsync(999, driver);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_RemovesDriver()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        var driver = new Driver
        {
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 575
        };
        context.Drivers.Add(driver);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.DeleteAsync(driver.DriverId);

        // Assert
        Assert.True(result);
        Assert.Empty(context.Drivers);
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ReturnsFalse()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new DriverRepository(context);

        // Act
        var result = await repository.DeleteAsync(999);

        // Assert
        Assert.False(result);
    }
}
