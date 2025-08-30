using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IJoinRepository<TEntity> where TEntity : class
{
    bool Exists(Func<TEntity, bool> predicate);
    Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate);
    Task InsertAsync(TEntity entity);
}