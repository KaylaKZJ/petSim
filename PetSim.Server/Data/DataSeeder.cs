using PetSim.Server.Data;
using PetSim.Server.Models;

public static class DataSeeder
{
    public static void Seed(PetSimContext context, IWebHostEnvironment env)
    {
        if (!context.PetTypes.Any())
        {
            SeedPetTypes(context, env);
        }
        if (!context.Pets.Any())
        {
            SeedPets(context, env);
        }
    }


    private static void SeedPets(PetSimContext context, IWebHostEnvironment env)
    {
        var petData = GetData<Pet>(env, "pets");

        foreach (var pet in petData)
        {
            if (pet.PetStats == null || pet.PetStats.Stats == null)
            {
                throw new Exception($"No pet stats data found, seeding failed");
            }

            // Assign Pet ID
            pet.Id = Guid.NewGuid();
            pet.Birthdate = DateTime.Now;

            // Ensure PetType exists, if not, create it
            var petType = context.PetTypes.FirstOrDefault(pt => pt.Type == pet.Type);
            if (petType == null)
            {
                petType = new PetType
                {
                    Id = Guid.NewGuid(),
                    Type = pet.Type
                };
                context.PetTypes.Add(petType);
            }

            // Assign PetTypeId (make sure it's pointing to a valid PetType)
            pet.PetTypeID = petType.Id;

            // Create PetStats and link it to the Pet
            var petStats = new PetStats
            {
                Id = Guid.NewGuid(),
                PetId = pet.Id,  // Link PetStats to this pet
                Stats = new Stats
                {
                    Happiness = pet.PetStats.Stats.Happiness,
                    Hunger = pet.PetStats.Stats.Hunger,
                    Boredom = pet.PetStats.Stats.Boredom,
                    Tiredness = pet.PetStats.Stats.Tiredness,
                    Weight = pet.PetStats.Stats.Weight,  // Now it's a decimal or double
                    Loneliness = pet.PetStats.Stats.Loneliness
                }
            };

            // Assign PetStatsId to the Pet
            pet.PetStatsId = petStats.Id;

            // Add the pet and its pet stats to the context
            context.PetStats.Add(petStats);
            context.Pets.Add(pet);
        }

        context.SaveChanges();
    }


    private static void SeedPetTypes(PetSimContext context, IWebHostEnvironment env)
    {


        var petTypeData = GetData<PetType>(env, "petTypes");

        if (!context.PetTypes.Any())
        {
            foreach (var petType in petTypeData)
            {

                petType.Id = Guid.NewGuid();
                petType.Type = petType.Type;

                context.PetTypes.Add(petType);
            }

            context.SaveChanges();
        }
    }


    private static List<T> GetData<T>(IWebHostEnvironment env, string fileName)
    {
        var jsonFilePath = Path.Combine(env.ContentRootPath, $"SeedData/{fileName}.json");

        if (!File.Exists(jsonFilePath))
        {
            throw new FileNotFoundException($"Seed data file not found at {jsonFilePath}");
        }

        var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(jsonFilePath));

        if (data == null)
        {
            throw new Exception($"No {fileName} data found, seeding failed");
        }

        return data;
    }
}
