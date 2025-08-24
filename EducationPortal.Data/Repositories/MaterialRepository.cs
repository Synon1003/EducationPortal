using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class MaterialRepository : EntityFrameworkRepository<Material>, IMaterialRepository
{
    public MaterialRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<Material>> GetMaterialsByCourseIdAsync(int courseId)
    {
        return await _context.Materials
            .AsNoTracking()
            .Where(m => m.CourseMaterials.Any(cm => cm.CourseId == courseId)).ToListAsync();
    }


    public async Task<ICollection<Video>> GetAllVideosAsync()
    {
        return await _context.Videos
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Video>> GetVideosWithMaterialByCourseIdAsync(int courseId)
    {
        var materials = _context.Materials
            .AsNoTracking()
            .Where(m => m.CourseMaterials.Any(cm => cm.CourseId == courseId));

        return await _context.Videos
            .AsNoTracking()
            .Include(m => m.Material)
            .Where(v => materials.Any(m => m.Id == v.MaterialId))
            .ToListAsync();
    }

    public async Task<Video?> GetVideoByMaterialIdAsync(int materialId)
    {
        return await _context.Videos
            .Include(v => v.Material)
            .FirstOrDefaultAsync(v => v.MaterialId == materialId);
    }


    public async Task<ICollection<Publication>> GetAllPublicationsAsync()
    {
        return await _context.Publications
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Publication>> GetPublicationsWithMaterialByCourseIdAsync(int courseId)
    {
        var materials = _context.Materials
            .AsNoTracking()
            .Where(m => m.CourseMaterials.Any(cm => cm.CourseId == courseId));

        return await _context.Publications
            .AsNoTracking()
            .Include(m => m.Material)
            .Where(v => materials.Any(m => m.Id == v.MaterialId))
            .ToListAsync();
    }

    public async Task<Publication?> GetPublicationByMaterialIdAsync(int materialId)
    {
        return await _context.Publications
            .Include(p => p.Material)
            .FirstOrDefaultAsync(v => v.MaterialId == materialId);
    }


    public async Task<ICollection<Article>> GetAllArticlesAsync()
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Article>> GetArticlesWithMaterialByCourseIdAsync(int courseId)
    {
        var materials = _context.Materials
            .AsNoTracking()
            .Where(m => m.CourseMaterials.Any(cm => cm.CourseId == courseId));

        return await _context.Articles
            .AsNoTracking()
            .Include(m => m.Material)
            .Where(v => materials.Any(m => m.Id == v.MaterialId))
            .ToListAsync();
    }

    public async Task<Article?> GetArticleByMaterialIdAsync(int materialId)
    {
        return await _context.Articles
            .Include(a => a.Material)
            .FirstOrDefaultAsync(v => v.MaterialId == materialId);
    }
}
