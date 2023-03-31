using System.Linq.Expressions;
using library_app_rest.Helpers;
using library_app_rest.Models;
using library_app_rest.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace library_app_rest.Repository;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    private readonly AppDbContext _db;

    public AuthorRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public new async Task<List<Author>> GetAll(Expression<Func<Author, bool>>? filter = null, int pageSize = 0,
        int pageNumber = 1, bool? include = false, bool? orderByBooks = false)
    {
        IQueryable<Author> query = DbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (pageSize > 0)
        {
            if (pageSize > 100) pageSize = 100;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        if (include == true) query = query.Include(x => x.Books).Include(x => x.User);
        if (orderByBooks == true) query = query.OrderByDescending(x => x.Books.Count);
        return await query.ToListAsync();
    }

    public new async Task<Author?> Get(Expression<Func<Author, bool>> filter = null, bool tracked = true,
        bool? include = false)
    {
        IQueryable<Author> query = DbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include == true) query = query.Include(x => x.Books).Include(x => x.User);
        return await query.FirstOrDefaultAsync();
    }

    public async Task Create(Author entity)
    {
        await _db.Authors.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Author> Update(Author entity)
    {
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == entity.Id);
        author.Name = entity.Name;
        author.Bio = entity.Bio;
        author.UserId = entity.UserId;
        _db.Authors.Update(author);
        await _db.SaveChangesAsync();
        return entity;
    }
}