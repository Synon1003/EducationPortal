namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICourseRepository CourseRepository { get; }
    IUserCourseRepository UserCourseRepository { get; }
    IUserMaterialRepository UserMaterialRepository { get; }
    IUserSkillRepository UserSkillRepository { get; }
    ISkillRepository SkillRepository { get; }
    IMaterialRepository MaterialRepository { get; }
    Task SaveChangesAsync();
}