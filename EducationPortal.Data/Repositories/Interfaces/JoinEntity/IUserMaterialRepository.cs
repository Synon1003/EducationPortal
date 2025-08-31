using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserMaterialRepository : IJoinRepository<UserMaterial>
{
    Task<ICollection<UserMaterial>> GetAllByUserIdAsync(Guid userId);
}