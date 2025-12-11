namespace F1DriverApp.Tests.Models;

public class DriverValidationTests
{
    [Fact]
    public void Driver_WithValidData_CreatesSuccessfully()
    {
        // Arrange & Act
        var driver = new Driver
        {
            DriverId = 1,
            Name = "Max Verstappen",
            Team = "Red Bull Racing",
            Country = "Netherlands",
            ChampionshipPoints = 575
        };

        // Assert
        Assert.Equal(1, driver.DriverId);
        Assert.Equal("Max Verstappen", driver.Name);
        Assert.Equal("Red Bull Racing", driver.Team);
        Assert.Equal("Netherlands", driver.Country);
        Assert.Equal(575, driver.ChampionshipPoints);
    }

    [Theory]
    [InlineData("", "Red Bull Racing", "Netherlands", 575, false)]
    [InlineData("Max Verstappen", "", "Netherlands", 575, false)]
    [InlineData("Max Verstappen", "Red Bull Racing", "", 575, false)]
    [InlineData("Max Verstappen", "Red Bull Racing", "Netherlands", 575, true)]
    public void DriverDto_Validation_ReturnsExpectedResult(
        string name, string team, string country, int points, bool isValid)
    {
        // Arrange
        var dto = new DriverDto
        {
            Name = name,
            Team = team,
            Country = country,
            ChampionshipPoints = points
        };

        // Act
        var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var actualIsValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
            dto, validationContext, validationResults, true);

        // Assert
        Assert.Equal(isValid, actualIsValid);
    }

    [Fact]
    public void DriverDto_WithNegativePoints_FailsValidation()
    {
        // Arrange
        var dto = new DriverDto
        {
            Name = "Test Driver",
            Team = "Test Team",
            Country = "Test Country",
            ChampionshipPoints = -10
        };

        // Act
        var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
            dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
        Assert.Contains(validationResults, 
            vr => vr.ErrorMessage!.Contains("Championship points must be greater than or equal to 0"));
    }

    [Fact]
    public void DriverDto_WithTooLongName_FailsValidation()
    {
        // Arrange
        var dto = new DriverDto
        {
            Name = new string('A', 101), // 101 characters
            Team = "Test Team",
            Country = "Test Country",
            ChampionshipPoints = 100
        };

        // Act
        var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(dto);
        var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
        var isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(
            dto, validationContext, validationResults, true);

        // Assert
        Assert.False(isValid);
    }
}
