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

    public async Task<bool> Exists(int id)
    {
        var entity = await GetByIdAsync(id);
        return entity != null;
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}