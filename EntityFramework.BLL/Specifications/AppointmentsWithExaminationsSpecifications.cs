using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class AppointmentsWithExaminationsSpecifications : BaseSpecification<Appointment>
{
    public AppointmentsWithExaminationsSpecifications(string sort)
    {
        AddInclude(x => x.Examinations);
        AddOrderBy(x => x.Title);

        if (string.IsNullOrEmpty(sort)) return;
        {
            switch (sort)
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