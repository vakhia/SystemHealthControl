using EntityFramework.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DAL.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<MedicalExamination> MedicalExaminations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}