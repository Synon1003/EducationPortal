using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;

namespace EducationPortal.Data.Repositories;

public class CourseRepository : EntityFrameworkRepository<Course>, ICourseRepository
{
    public CourseRepository(EducationPortalDbContext context) : base(context)
    { }

    public async Task<ICollection<Course>> GetAllCoursesWithSkillsAsync() =>
        await _context.Courses.AsNoTracking()
            .Include(c => c.Skills).ToListAsync();

    public async Task<ICollection<Course>> GetAllCoursesWithSkillsAndMaterialsAsync() =>
        await _context.Courses.AsNoTracking()
            .Include(c => c.Skills)
            .Include(c => c.Materials)
            .ToListAsync();

    public async Task<Course?> GetCourseWithSkillsAndMaterialsByIdAsync(int id) =>
        await _context.Courses.AsNoTracking()
            .Include(c => c.Skills)
            .Include(c => c.Materials)
            .SingleOrDefaultAsync(c => c.Id == id);

    public async Task<ICollection<Course>> GetCoursesByMaterialIdAsync(int materialId) =>
        await _context.Courses.AsNoTracking()
            .Include(c => c.Materials)
            .Where(c => c.Materials.Any(m => m.Id == materialId))
            .ToListAsync();
}
