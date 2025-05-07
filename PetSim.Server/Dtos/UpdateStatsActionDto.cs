using PetSim.Server.Models;

public class UpdateStatsActionDto
{
    public required string Type { get; set; }
    public required StatsDistribution StatsDistribution { get; set; }
}