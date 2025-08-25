namespace EducationPortal.Data.Repositories.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly EducationPortalDbContext _context;

    private ICourseRepository? _courseRepository = null;
    private ISkillRepository? _skillRepository = null;
    private IMaterialRepository? _materialRepository = null;

    public UnitOfWork(EducationPortalDbContext context)
    {
        _context = context;
    }

    public ICourseRepository CourseRepository =>
        _courseRepository ?? new CourseRepository(_context);

    public ISkillRepository SkillRepository =>
        _skillRepository ?? new SkillRepository(_context);

    public IMaterialRepository MaterialRepository =>
        _materialRepository ?? new MaterialRepository(_context);

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
