using EntityFramework.DAL.Models;

namespace EntityFramework.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> Repository<T>() where T : BaseModel;
    Task<int> Complete();

    Task DetachEntities(List<int> ids);
}