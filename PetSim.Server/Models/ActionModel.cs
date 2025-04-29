namespace PetSim.Server.Models;


public class StatsAction
{
    public Guid StatsActionId { get; set; }
    public required string Type { get; set; }
    public required StatsDistribution StatsDistribution { get; set; }

}
