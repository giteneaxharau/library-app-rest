using System.Linq.Expressions;
using library_app_rest.Models;

namespace library_app_rest.Repository.IRepository;

public interface IAuthorRepository : IRepository<Author>
{
    Task<List<Author>> GetAll(Expression<Func<Author, bool>>? filter = null, int pageSize = 0,
        int pageNumber = 1, bool? include = false, bool? orderByBooks = false);
    Task<Author> Update(Author entity);
    Task Create(Author entity);
}