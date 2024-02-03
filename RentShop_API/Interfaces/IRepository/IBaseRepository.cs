using System.Linq.Expressions;

namespace Interfaces.IRepository;

public interface IBaseRepository<T>
{
    Task<IQueryable<T>> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<bool> Exists(Guid id);
    Task Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
}