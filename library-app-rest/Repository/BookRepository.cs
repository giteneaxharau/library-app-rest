using System.Linq.Expressions;
using library_app_rest.Helpers;
using library_app_rest.Models;
using library_app_rest.Models.DTO.BookDTO;
using library_app_rest.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace library_app_rest.Repository;

public class BookRepository : Repository<Book>, IBookRepository
{
    private readonly AppDbContext _db;

    public BookRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public new async Task<List<Book>> GetAll(Expression<Func<Book, bool>>? filter = null, int pageSize = 0,
        int pageNumber = 1, bool? include = true)
    {
        IQueryable<Book> query = DbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (pageSize > 0)
        {
            if (pageSize > 100) pageSize = 100;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        if (include == true) query = query.Include(x => x.Categories).Include(x => x.Author);
        return await query.ToListAsync();
    }

    public new async Task<Book> Get(Expression<Func<Book, bool>> filter = null, bool tracked = true,
        bool? include = true)
    {
        IQueryable<Book> query = DbSet;
        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (include == true) query = query.Include(x => x.Categories).Include(x => x.Author);
        return await query.FirstOrDefaultAsync();
    }

    public new async Task Create(Book entity)
    {
        var selectedCategories = await _db.Categories.Where(x => entity.Categories.Contains(x)).ToListAsync();
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == entity.Author.Id);
        entity.Categories = selectedCategories;
        entity.AuthorId = author.Id;
        await _db.Books.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<Book> Update(Book entity)
    {
        var book = await _db.Books
            .Include(b => b.Categories)
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == entity.Id);
        var selectedCategories = await _db.Categories.Where(x => entity.Categories.Contains(x)).ToListAsync();
        var author = await _db.Authors.FirstOrDefaultAsync(a => a.Id == entity.Author.Id);
        book.Categories = selectedCategories;
        book.AuthorId = author.Id;
        book.Name = entity.Name;
        book.Description = entity.Description;
        book.UpdatedAt = DateTime.Now;
        _db.Books.Update(book);
        await _db.SaveChangesAsync();
        return entity;
    }
}