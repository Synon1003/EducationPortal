using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class CourseRepository : EntityFrameworkRepository<Course>, ICourseRepository
{
    public CourseRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<Course>> GetAllCoursesWithSkillsAndMaterialsAsync()
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(c => c.CourseSkills)
            .ThenInclude(cs => cs.Skill)
            .Include(c => c.CourseMaterials)
            .ThenInclude(cm => cm.Material)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseWithSkillsAsync(int id)
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(c => c.CourseSkills)
            .ThenInclude(cs => cs.Skill)
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ICollection<Course>> GetAstronautsByMaterialIdAsync(int materialId)
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(c => c.CourseMaterials)
            .ThenInclude(cm => cm.Material)
            .Where(c => c.CourseMaterials
                .Any(cm => cm.MaterialId == materialId))
            .ToListAsync();
    }
}
