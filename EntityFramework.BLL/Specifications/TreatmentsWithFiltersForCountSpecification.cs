using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class TreatmentsWithFiltersForCountSpecification : BaseSpecification<Treatment>
{
    public TreatmentsWithFiltersForCountSpecification(PaginationSpecificationParams specificationParams) : base(x
        => (string.IsNullOrEmpty(specificationParams.Search) || x.Title.ToLower().Contains(specificationParams.Search)))
    {
    }
}