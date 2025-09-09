using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null);
    Task<TEntity?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    void Insert(TEntity entity);
    void InsertRange(List<TEntity> entities);
}