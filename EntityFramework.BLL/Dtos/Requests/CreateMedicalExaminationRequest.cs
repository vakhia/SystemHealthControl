using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Dtos.Requests;

public class CreateMedicalExaminationRequest
{
    public string Title { get; set; }

    public string Description { get; set; }
    
    public IReadOnlyList<int> AppointmentsIds { get; set; }
}