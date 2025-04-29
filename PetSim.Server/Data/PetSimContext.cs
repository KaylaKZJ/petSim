using PetSim.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace PetSim.Server.Data;

public class PetSimContext : DbContext
{
    public PetSimContext(DbContextOptions<PetSimContext> options) : base(options) { }

    public DbSet<Pet> Pet => Set<Pet>();
    public DbSet<Stats> Stats => Set<Stats>();

    public DbSet<StatsDistribution> StatsDistribution => Set<StatsDistribution>();

    public DbSet<StatsAction> StatsAction => Set<StatsAction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stats>()
            .HasOne(s => s.Pet)
            .WithOne(p => p.Stats)
            .HasForeignKey<Stats>(s => s.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StatsDistribution>()
            .HasOne(s => s.StatsAction)
            .WithOne(a => a.StatsDistribution)
            .HasForeignKey<StatsAction>(s => s.StatsActionId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }


}