using System.Linq.Expressions;

namespace Interfaces.IRepository;

public interface IBaseRepository<T> where T : class
{
    Task<IQueryable<T>> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    Task<bool> Exists(Guid id);
    Task<bool> Exists(string name);
    Task Create(T entity);
    void Update(T entity);
    void Delete(Guid id);
}