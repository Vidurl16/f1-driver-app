using F1DriverApp.Api.Models;

namespace F1DriverApp.Api.Data;

public static class DbInitializer
{
    public static void Initialize(F1DriverContext context)
    {
        // Check if database already has data
        if (context.Drivers.Any())
        {
            return; // DB has been seeded
        }
        
        // Seed Teams
        var teams = new Team[]
        {
            new Team { Name = "Red Bull Racing" },
            new Team { Name = "Mercedes" },
            new Team { Name = "Ferrari" },
            new Team { Name = "McLaren" }
        };
        
        context.Teams.AddRange(teams);
        context.SaveChanges();
        
        // Seed Drivers
        var drivers = new Driver[]
        {
            new Driver
            {
                Name = "Max Verstappen",
                Team = "Red Bull Racing",
                Country = "Netherlands",
                ChampionshipPoints = 575,
                TeamId = teams[0].TeamId
            },
            new Driver
            {
                Name = "Lewis Hamilton",
                Team = "Mercedes",
                Country = "United Kingdom",
                ChampionshipPoints = 234,
                TeamId = teams[1].TeamId
            }
        };
        
        context.Drivers.AddRange(drivers);
        context.SaveChanges();
    }
}
