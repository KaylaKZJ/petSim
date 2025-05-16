namespace PetSim.Server.Models;


public class StatsAction
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required List<PetType> PetTypes { get; set; }
    public Guid StatsDistributionId { get; set; } = default!;
    public StatsDistribution StatsDistribution { get; set; } = default!;

}
