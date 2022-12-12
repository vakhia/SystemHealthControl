namespace EntityFramework.DAL.Models;

public class MedicalExamination : BaseModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    
    public ICollection<Treatment> Treatments { get; set; }
}