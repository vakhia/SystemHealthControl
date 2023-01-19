namespace EntityFramework.DAL.Models;

public class MedicalExaminationTreatments
{
    public int MedicalExaminationId;
    
    public MedicalExamination MedicalExamination;

    public int TreatmentId;
    
    public Treatment Treatment;
}