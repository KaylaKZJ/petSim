using System.Text.Json.Serialization;
namespace PetSim.Server.Models;


public class PetStats
{
    public Guid Id { get; set; }
    public Guid PetId { get; set; }

    [JsonIgnore]
    public Pet? Pet { get; set; }
    public required Stats Stats { get; set; }

}
