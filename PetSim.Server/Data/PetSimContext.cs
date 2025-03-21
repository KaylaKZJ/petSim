using PetSim.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace PetSim.Server.Data;

public class PetSimContext : DbContext {
    public PetSimContext (DbContextOptions<PetSimContext> options) : base(options) {}

    public DbSet<Pet> Pet => Set<Pet>();
    public DbSet<Stats> Stats => Set<Stats>();
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stats>()
            .HasOne(s => s.Pet)
            .WithOne(p => p.Stats) // Explicitly define the one-to-one relationship
            .HasForeignKey<Stats>(s => s.PetId)
            .OnDelete(DeleteBehavior.Cascade); // Delete stats on delete pet

        base.OnModelCreating(modelBuilder);  
    }


}