using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class MaterialRepository : EntityFrameworkRepository<Material>, IMaterialRepository
{
    public MaterialRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<Video>> GetVideosAsync()
    {
        return await _context.Videos
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Publication>> GetPublicationsAsync()
    {
        return await _context.Publications
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Article>> GetArticlesAsync()
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }
}
