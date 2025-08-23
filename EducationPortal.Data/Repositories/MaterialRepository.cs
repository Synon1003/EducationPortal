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

    public async Task<ICollection<Video>> GetVideosByCourseIdAsync(int courseId)
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

    public async Task<ICollection<Publication>> GetAllPublicationsAsync()
    {
        return await _context.Publications
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Publication>> GetPublicationsByCourseIdAsync(int courseId)
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

    public async Task<ICollection<Article>> GetAllArticlesAsync()
    {
        return await _context.Articles
            .AsNoTracking()
            .Include(m => m.Material)
            .ToListAsync();
    }

    public async Task<ICollection<Article>> GetArticlesByCourseIdAsync(int courseId)
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
}
