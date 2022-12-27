namespace Appointment.Aggregator.Models;

public class AppointmentModel
{
    public int Id { get; set; }

    public string DoctorId { get; set; }

    public string ClietId { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }

    public string? ExtraInformation { get; set; }
    
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }  
}