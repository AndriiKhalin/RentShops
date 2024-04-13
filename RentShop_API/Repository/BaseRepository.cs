using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Interfaces.IRepository;

namespace Repository;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly RentDbContext _context;

    public BaseRepository(RentDbContext context)
    {
        _context = context;
    }


    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(Guid id)
    {
        var result = _context.Set<T>().Find(id);
        _context.Set<T>().Remove(result);
    }

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).AsNoTracking();
    }

    public async Task<IQueryable<T>> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Attach(entity);
        _context.Set<T>().Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Update(entity);
    }

    public Task<bool> Exists(Guid id)
    {
        //return await _context.Set<T>().AnyAsync(x => x.Id == id);
        return Task.FromResult(true);
    }

    public async Task<bool> Exists(string name)
    {
        var entities = await _context.Set<T>().ToListAsync();
        return entities.Any(x => x.GetType().GetProperties().Any(p => p.PropertyType == typeof(string) && (string)p.GetValue(x) == name));
    }

    public Task<bool> ExistsNew(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().AnyAsync(expression);
    }
}