using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task InsertAsync(TEntity entity);
    Task InsertRangeAsync(List<TEntity> entities);
}