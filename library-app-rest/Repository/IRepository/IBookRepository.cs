using System.Linq.Expressions;
using library_app_rest.Models;

namespace library_app_rest.Repository.IRepository;

public interface IBookRepository: IRepository<Book>
{
    Task<Book> Update(Book entity);
    Task<List<Book>> GetAll(
        Expression<Func<Book, bool>>? filter = null,
        int pageSize = 0,
        int pageNumber = 1);
}