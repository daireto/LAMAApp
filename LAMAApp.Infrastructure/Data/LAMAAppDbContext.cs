using LAMAApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAMAApp.Infrastructure.Data;

public class LAMAAppDbContext : DbContext
{
    public LAMAAppDbContext(DbContextOptions<LAMAAppDbContext> options) : base(options)
    {
    }

    public DbSet<Miembro> Miembros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Miembro>(entity =>
        {
            entity.HasIndex(e => e.Member)
                .IsUnique()
                .HasDatabaseName("IX_Miembros_Member");
            
            entity.HasIndex(e => e.Cedula)
                .IsUnique()
                .HasDatabaseName("IX_Miembros_Cedula");
            
            entity.HasIndex(e => e.PlacaMatricula)
                .IsUnique()
                .HasDatabaseName("IX_Miembros_PlacaMatricula");
        });
    }
}
