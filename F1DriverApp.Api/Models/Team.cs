namespace F1DriverApp.Api.Models;

public class Team
{
    public int TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    
    // Navigation property
    public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
