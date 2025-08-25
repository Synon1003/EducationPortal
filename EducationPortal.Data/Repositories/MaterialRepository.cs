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
        return await _context.Materials.AsNoTracking()
            .Where(m => m.CourseMaterials.Any(cm => cm.CourseId == courseId))
            .ToListAsync();
    }


    public async Task<ICollection<Video>> GetAllVideosAsync() =>
        await _context.Videos.AsNoTracking().ToListAsync();

    public async Task<ICollection<Publication>> GetAllPublicationsAsync() => await _context.Publications.AsNoTracking().ToListAsync();

    public async Task<ICollection<Article>> GetAllArticlesAsync() =>
        await _context.Articles.AsNoTracking().ToListAsync();


    public async Task<Video?> GetVideoByMaterialIdAsync(int materialId) =>
        await _context.Videos.FirstOrDefaultAsync(v => v.Id == materialId);

    public async Task<Publication?> GetPublicationByMaterialIdAsync(int materialId) =>
        await _context.Publications.FirstOrDefaultAsync(v => v.Id == materialId);

    public async Task<Article?> GetArticleByMaterialIdAsync(int materialId) =>
        await _context.Articles.FirstOrDefaultAsync(v => v.Id == materialId);
}
