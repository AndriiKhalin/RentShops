using System.Linq.Expressions;

namespace Interfaces.IRepository;

public interface IBaseRepository<T> where T : class
{
    Task<IQueryable<T>> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<bool> Exists(Expression<Func<T, bool>> expression);
    Task Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
}