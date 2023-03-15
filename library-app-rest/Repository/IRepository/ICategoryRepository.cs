using System.Linq.Expressions;
using library_app_rest.Models;

namespace library_app_rest.Repository.IRepository;

public interface ICategoryRepository: IRepository<Category>
{
    Task Create(Category entity);
    Task<Category> Update(Category entity);
}