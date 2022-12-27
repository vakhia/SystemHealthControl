namespace Appointment.Aggregator.Models;

public class ExtendedAppointmentModel
{
    public int Id { get; set; }

    public UserModel Doctor { get; set; }

    public UserModel Client { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }

    public string? ExtraInformation { get; set; }
    
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }  
}