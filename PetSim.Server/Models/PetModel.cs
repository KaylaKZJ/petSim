using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PetSim.Server.Models;

public class Pet {
    public Guid Id { get; set; }
    public  string? Name { get; set; }
    public DateTime Birthdate { get; set; }

    // Navigation property for Stats
    public  Stats? Stats { get; set; }

    // Parameterless constructor for EF (needed by EF to create the object)
    public Pet() { }

    // Constructor ensures Stats is initialized
    public Pet(CreatePetDto CreatePetDto )
    {
        Id = Guid.NewGuid();
        Name = CreatePetDto.Name;
        Birthdate = DateTime.UtcNow;
        Stats = new Stats
        {
            Id = Guid.NewGuid(),
            PetId = Id,
            Happiness = 100,
            Hunger = 0,
            Boredom = 0
        };
    }
}
