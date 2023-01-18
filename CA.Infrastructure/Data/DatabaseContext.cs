using CA.Domain.Diseases;
using CA.Domain.Medicines;
using CA.Domain.Shared;
using CA.Domain.Suppliers;
using Microsoft.EntityFrameworkCore;

namespace CA.Infrastructure.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Medicine> Medicines { get; set; }

    public DbSet<Disease> Diseases { get; set; }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<MedicineDisease> MedicineDiseases { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MedicineDisease>(x => x.HasKey(md => new { md.MedicineId, md.DiseaseId }));

        modelBuilder.Entity<MedicineDisease>()
            .HasOne(m => m.Medicine)
            .WithMany(d => d.Diseases)
            .HasForeignKey(md => md.MedicineId);
        
        modelBuilder.Entity<MedicineDisease>()
            .HasOne(d => d.Disease)
            .WithMany(m => m.Medicines)
            .HasForeignKey(md => md.DiseaseId);
        
        
    }
}