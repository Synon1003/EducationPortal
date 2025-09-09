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

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task InsertRangeAsync(List<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }
}