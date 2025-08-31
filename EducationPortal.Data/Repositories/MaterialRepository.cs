using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.Data.Repositories;

public class MaterialRepository : EntityFrameworkRepository<Material>, IMaterialRepository
{
    public MaterialRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<Material>> GetMaterialsByCourseIdAsync(int courseId) =>
        await _context.Materials.AsNoTracking()
            .Where(m => m.Courses.Any(c => c.Id == courseId)).ToListAsync();

    public async Task<ICollection<Video>> GetAcquiredVideosByUserIdAsync(Guid userId) =>
        await _context.Videos.AsNoTracking().Where(p => p.AcquiredByUsers.Any(u => u.Id == userId)).ToListAsync();

    public async Task<ICollection<Publication>> GetAcquiredPublicationsByUserIdAsync(Guid userId) =>
        await _context.Publications.AsNoTracking().Where(p => p.AcquiredByUsers.Any(u => u.Id == userId)).ToListAsync();

    public async Task<ICollection<Article>> GetAcquiredArticlesByUserIdAsync(Guid userId) =>
        await _context.Articles.AsNoTracking().Where(p => p.AcquiredByUsers.Any(u => u.Id == userId)).ToListAsync();

    public async Task<ICollection<Video>> GetVideosCreatedByUserIdAsync(Guid userId) =>
        await _context.Videos.AsNoTracking().ToListAsync(); // TODO: later create CreatedBy prop for Material

    public async Task<Video?> GetVideoByMaterialIdAsync(int materialId) =>
        await _context.Videos.FirstOrDefaultAsync(v => v.Id == materialId);

    public async Task<Publication?> GetPublicationByMaterialIdAsync(int materialId) =>
        await _context.Publications.FirstOrDefaultAsync(v => v.Id == materialId);

    public async Task<Article?> GetArticleByMaterialIdAsync(int materialId) =>
        await _context.Articles.FirstOrDefaultAsync(v => v.Id == materialId);
}
