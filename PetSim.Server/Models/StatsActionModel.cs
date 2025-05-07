namespace PetSim.Server.Models;


public class StatsAction
{
    public Guid StatsActionId { get; set; }
    public required string Type { get; set; }
    public Guid PetTypeID { get; set; }
    public StatsDistribution StatsDistribution { get; set; } = default!;

}
