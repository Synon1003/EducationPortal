using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Repositories.Interfaces;

namespace EducationPortal.Data.Repositories;

public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected EducationPortalDbContext _context;

    public EntityFrameworkRepository(EducationPortalDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public bool Exists(Func<TEntity, bool> predicate)
    {
        return _context.Set<TEntity>().AsNoTracking().Any(predicate);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task InsertRangeAsync(List<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }
}