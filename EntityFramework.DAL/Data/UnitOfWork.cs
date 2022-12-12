using System.Collections;
using EntityFramework.DAL.Interfaces;
using EntityFramework.DAL.Models;
using EntityFramework.DAL.Repositories;

namespace EntityFramework.DAL.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _databaseContext;

    private Hashtable _repositories;

    public UnitOfWork(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Dispose() 
    {
        _databaseContext.Dispose();
    }

    public IGenericRepository<T> Repository<T>() where T : BaseModel
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _databaseContext);
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public async Task<int> Complete()
    {
        return await _databaseContext.SaveChangesAsync();
    }
}