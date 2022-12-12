using EntityFramework.DAL.Models;

namespace EntityFramework.DAL.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpec(ISpecification<T> specification);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification);
    Task<int> CountAsync(ISpecification<T> specification);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}