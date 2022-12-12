using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class MedicalExaminationsWithAppointmentSpecification : BaseSpecification<MedicalExamination>
{
    public MedicalExaminationsWithAppointmentSpecification(PaginationSpecificationParams specificationParams) : base(x
        => (string.IsNullOrEmpty(specificationParams.Search) || x.Title.ToLower().Contains(specificationParams.Search)))
    {
        AddInclude(m => m.Appointments);
        AddOrderBy(x => x.Title);
        ApplyPaging(specificationParams.PageSize * (specificationParams.PageIndex - 1),
            specificationParams.PageSize);

        if (string.IsNullOrEmpty(specificationParams.Sort)) return;
        {
            switch (specificationParams.Sort)
            {
                case "titleAsc":
                    AddOrderBy(x => x.Title);
                    break;
                case "titleDesc":
                    AddOrderByDescending(x => x.Title);
                    break;
                default:
                    AddOrderBy(x => x.Title);
                    break;
            }
        }
    }

    public MedicalExaminationsWithAppointmentSpecification(int id) :
        base(me => me.Id == id)
    {
        AddInclude(m => m.Appointments);
    }
}