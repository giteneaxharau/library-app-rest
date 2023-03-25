using System.Linq.Expressions;
using library_app_rest.Helpers;
using library_app_rest.Models;
using library_app_rest.Models.DTO.BookDTO;
using library_app_rest.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace library_app_rest.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }
    
    public new async Task<List<Category>> GetAll(Expression<Func<Category, bool>>? filter = null, int pageSize = 0, int pageNumber = 1,bool? include = true)
    {
        IQueryable<Category> query = DbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (pageSize > 0)
        {
            if(pageSize>100) pageSize = 100;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        if (include == true) query = query.Include(x => x.Books);
        return await query.ToListAsync();
    }
    
    public new async Task<Category> Get(Expression<Func<Category, bool>> filter = null, bool tracked = true, bool? include = true)
    {
        IQueryable<Category> query = DbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include == true) query = query.Include(x => x.Books).ThenInclude(b=>b.Author);
        return await query.FirstOrDefaultAsync();
    }
    public new async Task Create(Category entity)
    {
        var selectedBooks = await _db.Books.Where(x => entity.Books.Contains(x)).ToListAsync();
        entity.Books = selectedBooks;
        await _db.Categories.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Category> Update(Category entity)
    {
        var category = await _db.Categories
            .Include(b => b.Books)
            .FirstOrDefaultAsync(b => b.Id == entity.Id);
        var selectedBooks = await _db.Books.Where(x => entity.Books.Contains(x)).ToListAsync();
        category.Books = selectedBooks;
        category.Name = entity.Name;
        category.UpdatedAt = DateTime.Now;
        category.Priority = entity.Priority;
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
        return entity;
    }
}