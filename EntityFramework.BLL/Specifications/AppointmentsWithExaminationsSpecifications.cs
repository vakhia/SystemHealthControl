using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class AppointmentsWithExaminationsSpecifications: BaseSpecification<Appointment>
{
    public AppointmentsWithExaminationsSpecifications()
    {
        AddInclude(x => x.Examinations);
    }

    public AppointmentsWithExaminationsSpecifications(int id): base(x => x.Id == id)
    {
        AddInclude(x => x.Examinations);
    }
}