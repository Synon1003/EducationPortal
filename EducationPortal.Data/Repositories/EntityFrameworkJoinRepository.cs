using EducationPortal.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class EntityFrameworkJoinRepository<TEntity> : IJoinRepository<TEntity> where TEntity : class
{
    protected EducationPortalDbContext _context;

    public EntityFrameworkJoinRepository(EducationPortalDbContext context)
    {
        _context = context;
    }

    public bool Exists(Func<TEntity, bool> predicate)
    {
        return _context.Set<TEntity>().AsNoTracking().Any(predicate);
    }

    public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AsNoTracking().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
