namespace EntityFramework.BLL.Dtos.Responses;

public class TreatmentResponse
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string? ExtraInformation { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EstimatedEndDate { get; set; }
    
    public int DoctorId { get; set; }
    
    public int ClientId { get; set; }
}