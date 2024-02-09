using Entities;
using Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        _context.Set<T>().Update(entity);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _context.Set<T>().AnyAsync(x => EF.Property<Guid>(x, "Id") == id);
    }

}