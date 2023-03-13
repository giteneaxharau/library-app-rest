using System.Linq.Expressions;

namespace library_app_rest.Repository.IRepository;

public interface IRepository<T> where T :class
{
   Task<List<T>> GetAll(Expression<Func<T,bool>>? filter = null, int pageSize = 0, int pageNumber = 1); 
   Task<T> Get(Expression<Func<T, bool>> filter = null, bool tracked = true);
   Task Create(T entity);
   Task Remove(T entity);
   Task Save();
}
