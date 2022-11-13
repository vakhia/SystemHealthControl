using EntityFramework.DAL.Data;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly DatabaseContext _databaseContext;

    public GenericRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _databaseContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _databaseContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetEntityWithSpec(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
    {
        return await ApplySpecification(specification).ToListAsync();
    }
    
    private IQueryable<T> ApplySpecification(ISpecification<T> specification)
    {
        return SpecificationEvaluator<T>.GetQuery(_databaseContext.Set<T>().AsQueryable(), specification);
    }
}