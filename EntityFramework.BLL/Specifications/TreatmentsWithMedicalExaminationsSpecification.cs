using System.Linq.Expressions;
using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class TreatmentsWithMedicalExaminationsSpecification : BaseSpecification<Treatment>
{
    public TreatmentsWithMedicalExaminationsSpecification(PaginationSpecificationParams specificationParams) : base(x
        => (string.IsNullOrEmpty(specificationParams.Search) || x.Title.ToLower().Contains(specificationParams.Search)))
    {
        AddInclude(m => m.Examinations);
        AddOrderBy(x => x.StartDate);
        ApplyPaging(specificationParams.PageSize * (specificationParams.PageIndex - 1),
            specificationParams.PageSize);

        if (string.IsNullOrEmpty(specificationParams.Sort)) return;
        {
            switch (specificationParams.Sort)
            {
                case "startDateAsc":
                    AddOrderBy(x => x.StartDate);
                    break;
                case "startDateDesc":
                    AddOrderByDescending(x => x.StartDate);
                    break;
                default:
                    AddOrderBy(x => x.StartDate);
                    break;
            }
        }
    }

    public TreatmentsWithMedicalExaminationsSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.Examinations);
    }
}