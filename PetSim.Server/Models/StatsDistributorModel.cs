namespace PetSim.Server.Models;


public class StatsDistribution
{
    public Guid Id { get; set; }
    public required Guid StatsActionId { get; set; }
    public StatsAction StatsAction { get; set; } = default!;
    public int Happiness { get; set; }
    public int Hunger { get; set; }
    public int Boredom { get; set; }

}
