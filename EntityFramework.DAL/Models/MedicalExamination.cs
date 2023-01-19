namespace EntityFramework.DAL.Models;

public class MedicalExamination : BaseModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<AppointmentMedicalExamination> Appointments { get; set; } =
        new List<AppointmentMedicalExamination>();

    public ICollection<MedicalExaminationTreatments> Treatments { get; set; } =
        new List<MedicalExaminationTreatments>();
}