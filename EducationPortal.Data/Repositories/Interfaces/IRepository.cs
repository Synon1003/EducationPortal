namespace EducationPortal.Data.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<bool> Exists(int id);
    Task InsertAsync(TEntity entity);
}