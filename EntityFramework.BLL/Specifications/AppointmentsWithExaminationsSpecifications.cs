using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class AppointmentsWithExaminationsSpecifications : BaseSpecification<Appointment>
{
    public AppointmentsWithExaminationsSpecifications(AppointmentSpecParams appointmentParams) : base(x
        => (string.IsNullOrEmpty(appointmentParams.Search) || x.Title.ToLower().Contains(appointmentParams.Search)))
    {
        AddInclude(x => x.Examinations);
        AddOrderBy(x => x.Title);
        ApplyPaging(appointmentParams.PageSize * (appointmentParams.PageIndex - 1),
            appointmentParams.PageSize);

        if (string.IsNullOrEmpty(appointmentParams.Sort)) return;
        {
            switch (appointmentParams.Sort)
            {
                case "startDateAsc":
                    AddOrderBy(x => x.StartDate);
                    break;
                case "startDateDesc":
                    AddOrderByDescending(x => x.StartDate);
                    break;
                default:
                    AddOrderBy(x => x.Title);
                    break;
            }
        }
    }

    public AppointmentsWithExaminationsSpecifications(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Examinations);
    }
}