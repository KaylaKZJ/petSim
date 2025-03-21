using System.Text.Json.Serialization;
namespace PetSim.Server.Models;


public class Stats {
    public Guid Id { get; set; }
    public int Happiness { get; set; }
    public int Hunger { get; set; }
    public int Boredom { get; set; }

    // Foreign key reference back to Pet
    public Guid PetId { get; set; }
    
    [JsonIgnore]
    public  Pet? Pet { get; set; }
}
