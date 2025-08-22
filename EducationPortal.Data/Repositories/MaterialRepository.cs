using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class MaterialRepository : EntityFrameworkRepository<Material>, IMaterialRepository
{
    public MaterialRepository(EducationPortalDbContext context) : base(context)
    { }
}
