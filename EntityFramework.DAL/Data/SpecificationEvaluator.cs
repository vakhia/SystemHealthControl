﻿using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DAL.Data;

public class SpecificationEvaluator<T> where T : BaseModel
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}