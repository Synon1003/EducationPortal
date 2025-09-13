using Microsoft.EntityFrameworkCore;
using EducationPortal.Data.Repositories.Interfaces;
using EducationPortal.Data.Entities;
using System.Linq.Expressions;

namespace EducationPortal.Data.Repositories;

public class CourseRepository : EntityFrameworkRepository<Course>, ICourseRepository
{
    public CourseRepository(EducationPortalDbContext context) : base(context)
    { }

    private IQueryable<Course> GetCoursesWithSkillsAndUserByFilter(Expression<Func<Course, bool>> predicate)
    {
        return GetAll()
            .Include(c => c.Skills)
            .Include(c => c.CreatedByUser)
            .Where(predicate)
            .OrderByDescending(c => c.Id);
    }

    public async Task<ICollection<Course>> GetAllCoursesWithSkillsAsync() =>
        await GetCoursesWithSkillsAndUserByFilter(_ => true).ToListAsync();

    public async Task<ICollection<Course>> GetAvailableCoursesWithSkillsForUserAsync(Guid userId) =>
        await GetCoursesWithSkillsAndUserByFilter(c => !c.UserCourses
            .Any(uc => uc.UserId == userId))
            .ToListAsync();

    public async Task<ICollection<Course>> GetInProgressCoursesWithSkillsForUserAsync(Guid userId) =>
        await GetCoursesWithSkillsAndUserByFilter(c => c.UserCourses
            .Any(uc => uc.UserId == userId && uc.ProgressPercentage != 100))
            .ToListAsync();

    public async Task<ICollection<Course>> GetCompletedCoursesWithSkillsForUserAsync(Guid userId) =>
        await GetCoursesWithSkillsAndUserByFilter(c => c.UserCourses
            .Any(uc => uc.UserId == userId && uc.ProgressPercentage == 100))
            .ToListAsync();

    public async Task<ICollection<Course>> GetCreatedCoursesWithSkillsForUserAsync(Guid userId) =>
        await GetCoursesWithSkillsAndUserByFilter(c => c.CreatedBy == userId).ToListAsync();

    public async Task<ICollection<Course>> GetAllCoursesWithSkillsAndMaterialsAsync() =>
        await GetAll()
            .Include(c => c.Skills)
            .Include(c => c.Materials)
            .ToListAsync();

    public async Task<Course?> GetCourseWithRelationshipsByIdAsync(int id) =>
        await GetAll()
            .Include(c => c.Skills)
            .Include(c => c.Materials)
            .Include(c => c.CreatedByUser)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<ICollection<Course>> GetCoursesByMaterialIdAsync(int materialId) =>
        await GetAll()
            .Include(c => c.Materials)
            .Where(c => c.Materials.Any(m => m.Id == materialId))
            .ToListAsync();
}
