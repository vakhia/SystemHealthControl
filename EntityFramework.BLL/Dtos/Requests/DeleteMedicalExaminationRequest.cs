namespace EntityFramework.BLL.Dtos.Requests;

public class DeleteMedicalExaminationRequest
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }
}