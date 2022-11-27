using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class AppointmentWithFiltersForCountSpecification : BaseSpecification<Appointment>
{
    public AppointmentWithFiltersForCountSpecification(AppointmentSpecParams appointmentParams) : base(x
        => (string.IsNullOrEmpty(appointmentParams.Search) || x.Title.ToLower().Contains(appointmentParams.Search)))
    {
    }
}