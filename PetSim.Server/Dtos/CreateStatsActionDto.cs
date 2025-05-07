using PetSim.Server.Models;

public class CreateStatsActionDto
{
    public required string Type { get; set; }
    public required string PetType { get; set; }
    public required StatsDistribution StatsDistribution { get; set; }
}