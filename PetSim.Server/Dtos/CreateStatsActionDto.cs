using PetSim.Server.Models;

public class CreateStatsActionDto
{
    public required string Type { get; set; }
    public required List<PetType> PetTypes { get; set; }
    public required StatsDistribution StatsDistribution { get; set; }
}