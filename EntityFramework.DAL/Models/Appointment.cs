namespace EntityFramework.DAL.Models;

public class Appointment : BaseModel
{
    public int DoctorId { get; set; }

    public int ClientId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string? ExtraInformation { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ICollection<MedicalExamination> Examinations { get; set; }
}