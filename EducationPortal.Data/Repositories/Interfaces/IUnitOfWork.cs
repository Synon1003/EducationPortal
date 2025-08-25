namespace EducationPortal.Data.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICourseRepository CourseRepository { get; }
    ISkillRepository SkillRepository { get; }
    IMaterialRepository MaterialRepository { get; }
    Task SaveChangesAsync();
}