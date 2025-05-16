using PetSim.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace PetSim.Server.Data;

public class PetSimContext : DbContext
{
    public PetSimContext(DbContextOptions<PetSimContext> options) : base(options) { }

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<PetStats> PetStats => Set<PetStats>();
    public DbSet<PetType> PetTypes => Set<PetType>();
    public DbSet<StatsDistribution> StatsDistribution => Set<StatsDistribution>();
    public DbSet<StatsAction> StatsAction => Set<StatsAction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PetStats>()
            .HasOne(s => s.Pet)
            .WithOne(p => p.PetStats)
            .HasForeignKey<PetStats>(s => s.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StatsDistribution>()
            .HasOne(s => s.StatsAction)
            .WithOne(a => a.StatsDistribution)
            .HasForeignKey<StatsAction>(s => s.Id)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StatsDistribution>()
            .OwnsOne(sd => sd.Stats);

        modelBuilder.Entity<PetStats>()
            .OwnsOne(sd => sd.Stats);

        base.OnModelCreating(modelBuilder);
    }


}