using library_app_rest.Models;

namespace library_app_rest.Repository.IRepository;

public interface IAuthorRepository : IRepository<Author>
{
    Task<Author> Update(Author entity);
    Task Create(Author entity);
}