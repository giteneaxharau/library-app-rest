using System.Linq.Expressions;
using library_app_rest.Helpers;
using library_app_rest.Models;
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

    public new async Task<List<Book>> GetAll(
        Expression<Func<Book, bool>>? filter = null,
        int pageSize = 0,
        int pageNumber = 1)
    {
        if (filter != null) _db.Books.Where(filter);
        if (pageSize > 0)
        {
            if (pageSize > 100) pageSize = 100;
            _db.Books.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        // var books = _db.Books
            // .Include(b => b.BooksCategories).ThenInclude(bc=>bc.Category)
            // .ToListAsync();
        return await _db.Books.ToListAsync();
    }

    public async Task<Book> Update(Book entity)
    {
        entity.UpdatedAt = DateTime.Now;
        _db.Books.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}