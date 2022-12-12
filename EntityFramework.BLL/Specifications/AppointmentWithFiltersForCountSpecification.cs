using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class AppointmentWithFiltersForCountSpecification : BaseSpecification<Appointment>
{
    public AppointmentWithFiltersForCountSpecification(PaginationSpecificationParams specificationParams) : base(x
        => (string.IsNullOrEmpty(specificationParams.Search) || x.Title.ToLower().Contains(specificationParams.Search)))
    {
    }
}