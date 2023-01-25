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
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<AppointmentMedicalExamination>(x => x.HasKey(amd => new { amd.Id }));

        modelBuilder.Entity<AppointmentMedicalExamination>()
            .HasOne(m => m.Appointment)
            .WithMany(d => d.Examinations)
            .HasForeignKey(md => md.AppointmentId);
        
        modelBuilder.Entity<AppointmentMedicalExamination>()
            .HasOne(d => d.MedicalExamination)
            .WithMany(m => m.Appointments)
            .HasForeignKey(md => md.MedicalExaminationId);
        
        
        modelBuilder.Entity<MedicalExaminationTreatments>(x => x.HasKey(met => new { met.Id }));

        modelBuilder.Entity<MedicalExaminationTreatments>()
            .HasOne(m => m.MedicalExamination)
            .WithMany(d => d.Treatments)
            .HasForeignKey(md => md.MedicalExaminationId);
        
        modelBuilder.Entity<MedicalExaminationTreatments>()
            .HasOne(d => d.Treatment)
            .WithMany(m => m.Examinations)
            .HasForeignKey(md => md.TreatmentId);

        
    }
}