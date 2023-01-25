namespace EntityFramework.DAL.Models;

public class AppointmentMedicalExamination : BaseModel
{
    public int AppointmentId { get; set; }
    
    public Appointment Appointment { get; set; }

    public int MedicalExaminationId { get; set; }
    
    public MedicalExamination MedicalExamination { get; set; }
}