﻿using EntityFramework.DAL.Models;

namespace EntityFramework.BLL.Specifications;

public class AppointmentsWithExaminationsSpecifications : BaseSpecification<Appointment>
{
    public AppointmentsWithExaminationsSpecifications(PaginationSpecificationParams specificationParams) : base(x
        => (string.IsNullOrEmpty(specificationParams.Search) || x.Title.ToLower().Contains(specificationParams.Search)))
    {
        AddInclude(x => x.Examinations);
        AddOrderBy(x => x.Title);
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