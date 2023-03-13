using System.Linq.Expressions;
using library_app_rest.Helpers;
using library_app_rest.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace library_app_rest.Repository;

public class Repository<T>: IRepository<T> where T : class
{

    private readonly AppDbContext db;
    internal DbSet<T> dbSet;
    
    public Repository(AppDbContext db)
    {
        this.db = db;
        this.dbSet = db.Set<T>();
    }


    public Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1)
    {
        throw new NotImplementedException();
    }

    public Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true)
    {
        throw new NotImplementedException();
    }

    public Task Create(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }
}