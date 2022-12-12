namespace EntityFramework.DAL.Models;

public class MedicalExaminationTreatments : BaseModel
{
    public int MedicalExaminationId;
    
    public MedicalExamination MedicalExamination;

    public int TreatmentId;
    
    public Treatment Treatment;
}