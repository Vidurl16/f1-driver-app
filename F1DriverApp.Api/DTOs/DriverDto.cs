using System.ComponentModel.DataAnnotations;

namespace F1DriverApp.Api.DTOs;

public class DriverDto
{
    public int DriverId { get; set; }
    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Team is required")]
    [StringLength(100, ErrorMessage = "Team cannot exceed 100 characters")]
    public string Team { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Country is required")]
    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string Country { get; set; } = string.Empty;
    
    [Range(0, int.MaxValue, ErrorMessage = "Championship points must be greater than or equal to 0")]
    public int ChampionshipPoints { get; set; }
}
