using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected EducationPortalDbContext _context;

    public EntityFrameworkRepository(EducationPortalDbContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().AsNoTracking();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
    }

    public void Insert(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void InsertRange(List<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }
}