namespace F1DriverApp.Api.Models;

public class Driver
{
    public int DriverId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Team { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public int ChampionshipPoints { get; set; }
    
    // Foreign key to Team table
    public int? TeamId { get; set; }
    public Team? TeamEntity { get; set; }
}
