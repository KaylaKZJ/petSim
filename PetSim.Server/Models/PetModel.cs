namespace PetSim.Server.Models;

public class Pet
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid PetTypeID { get; set; }
    public string? Type { get; set; }
    public DateTime Birthdate { get; set; }

    // Navigation property for Stats
    public PetStats? PetStats { get; set; }
    public Guid? PetStatsId { get; set; }

    // Parameterless constructor for EF (needed by EF to create the object)
    public Pet() { }

    // Constructor ensures Stats is initialized
    public Pet(CreatePetDto CreatePetDto, string petType)
    {
        Id = Guid.NewGuid();
        Name = CreatePetDto.Name;
        Type = petType;
        Birthdate = DateTime.UtcNow;
        PetStats = new PetStats
        {
            Id = Guid.NewGuid(),
            PetId = Id,
            Stats = new Stats
            {
                Happiness = 100,
                Hunger = 0,
                Boredom = 0,
                Tiredness = 0,
                Weight = 0,
                Loneliness = 0,
            }
        };
    }
}
