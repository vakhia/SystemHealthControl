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

    public DbSet<AppointmentMedicalExamination> AppointmentMedicalExaminations { get; set; }

    public DbSet<Treatment> Treatments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppointmentMedicalExamination>()
            .HasKey(i => new { i.AppointmentId, i.MedicalExaminationId });

        modelBuilder.Entity<MedicalExaminationTreatments>()
            .HasKey(i => new { i.MedicalExaminationId, i.TreatmentId });
    }
}