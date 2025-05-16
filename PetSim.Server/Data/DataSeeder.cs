using PetSim.Server.Data;
using PetSim.Server.Models;

public static class DataSeeder
{
    public static void Seed(PetSimContext context, IWebHostEnvironment env)
    {
        // SeedPets(context, env);
        SeedStatsActions(context, env);
        // SeedPetTypes(context, env);
    }

    private static void SeedPets(PetSimContext context, IWebHostEnvironment env)
    {
        if (!context.Pets.Any())
        {

            var petData = GetData<Pet>(env, "pets");

            foreach (var data in petData)
            {
                if (data.PetStats?.Stats == null)
                    throw new Exception("No pet stats data found, seeding failed.");

                var petId = Guid.NewGuid();

                // Get or create the PetType
                var petType = context.PetTypes.FirstOrDefault(pt => pt.Type == data.Type);
                if (petType == null)
                    throw new Exception($"No pet type found for {data.Type}, seeding failed.");

                var stats = data.PetStats.Stats;
                var petStats = new PetStats
                {
                    Id = Guid.NewGuid(),
                    PetId = petId,
                    Stats = new Stats
                    {
                        Happiness = stats.Happiness,
                        Hunger = stats.Hunger,
                        Boredom = stats.Boredom,
                        Tiredness = stats.Tiredness,
                        Weight = stats.Weight,
                        Loneliness = stats.Loneliness
                    }
                };

                var pet = new Pet
                {
                    Id = petId,
                    Name = data.Name,
                    Type = data.Type,
                    Birthdate = DateTime.Now,
                    PetTypeID = petType.Id,
                    PetStats = petStats,
                    PetStatsId = petStats.Id
                };

                context.PetStats.Add(petStats);
                context.Pets.Add(pet);
            }

            context.SaveChanges();
        }
    }


    private static void SeedStatsActions(PetSimContext context, IWebHostEnvironment env)
    {

        if (!context.StatsAction.Any())
        {
            var statsActionsData = GetData<StatsAction>(env, "statsAction");

            foreach (var data in statsActionsData)
            {
                var stats = data.StatsDistribution;
                var statsDistributionId = Guid.NewGuid();
                var statsActionId = Guid.NewGuid();

                var statsDistribution = new StatsDistribution
                {
                    Id = statsDistributionId,
                    StatsActionId = statsActionId,
                    Stats = stats.Stats,

                };


                var statsAction = new StatsAction
                {
                    Id = statsActionId,
                    Type = data.Type,
                    PetTypes = data.PetTypes ?? new List<PetType>(),
                    StatsDistribution = statsDistribution,
                    StatsDistributionId = statsDistributionId
                };

                statsDistribution.StatsAction = statsAction;

                context.StatsAction.Add(statsAction);
                context.StatsDistribution.Add(statsDistribution);
                context.SaveChanges();

            }
        }
    }

    private static void SeedPetTypes(PetSimContext context, IWebHostEnvironment env)
    {
        if (!context.PetTypes.Any())
        {
            var petTypeData = GetData<PetType>(env, "petTypes");

            foreach (var data in petTypeData)
            {
                var petType = new PetType
                {

                    Id = Guid.NewGuid(),
                    Type = data.Type,
                };
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
