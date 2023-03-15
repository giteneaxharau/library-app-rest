using System.Linq.Expressions;

namespace library_app_rest.Repository.IRepository;

public interface IRepository<T> where T :class
{
   Task<List<T>> GetAll(Expression<Func<T,bool>>? filter = null, int pageSize = 0, int pageNumber = 1, bool? include = true);
   Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true, bool? include = true);
   Task Create(T entity);
   Task Remove(T entity);
   Task Save();
}
