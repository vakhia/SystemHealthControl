using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class MedicalExaminationsWithFiltersForCountSpecification : BaseSpecification<MedicalExamination>
{
    public MedicalExaminationsWithFiltersForCountSpecification(PaginationSpecificationParams specificationParams) : base(x
        => (string.IsNullOrEmpty(specificationParams.Search) || x.Title.ToLower().Contains(specificationParams.Search)))
    {
    }
}