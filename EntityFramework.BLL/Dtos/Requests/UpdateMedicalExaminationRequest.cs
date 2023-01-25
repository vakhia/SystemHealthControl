namespace EntityFramework.BLL.Dtos.Requests;

public class UpdateMedicalExaminationRequest
{
    public int Id { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }
    
    public IList<int> AppointmentsIds { get; set; }
}