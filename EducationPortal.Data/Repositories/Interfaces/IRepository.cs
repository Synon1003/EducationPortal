namespace EducationPortal.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    bool Exists(Func<TEntity, bool> predicate);
    Task InsertAsync(TEntity entity);
    Task InsertRangeAsync(List<TEntity> entities);
    Task UpdateAsync(TEntity entity);
}