using System.Linq.Expressions;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUserMaterialRepository
{
    bool Exists(Func<UserMaterial, bool> predicate);
    Task<UserMaterial?> GetByFilterAsync(Expression<Func<UserMaterial, bool>> predicate);
    Task InsertAsync(UserMaterial userMaterial);
}