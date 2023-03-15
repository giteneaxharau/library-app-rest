using System.Linq.Expressions;
using library_app_rest.Helpers;
using library_app_rest.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace library_app_rest.Repository;

public class Repository<T>: IRepository<T> where T : class
{

    private readonly AppDbContext _db;
    internal DbSet<T> DbSet;
    
    public Repository(AppDbContext db)
    {
        _db = db;
        this.DbSet = db.Set<T>();
    }


    public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, int pageSize = 0, int pageNumber = 1,bool? include = true)
    {
        IQueryable<T> query = DbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (pageSize > 0)
        {
            if(pageSize>100) pageSize = 100;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
        return await query.ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, bool? include = true)
    {
        IQueryable<T> query = DbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task Create(T entity)
    {
        await DbSet.AddAsync(entity);
        await Save();
    }

    public async Task Remove(T entity)
    {
        DbSet.Remove(entity);
        await Save();
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}